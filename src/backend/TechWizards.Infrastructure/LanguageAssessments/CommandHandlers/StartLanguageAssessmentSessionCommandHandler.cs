using AutoMapper;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.General.Time.Provider.Brokers;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.GrammarAssessments.Services;
using TechWizards.Application.LanguageAssessments.Commands;
using TechWizards.Application.LanguageAssessments.Events;
using TechWizards.Application.ListeningAssessments.Services;
using TechWizards.Application.QuizAssessmentSessions.Models;
using TechWizards.Application.QuizAssessmentSessions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;
using TechWizards.Infrastructure.Common.Constants;
using Temporary.Extensions;

namespace TechWizards.Infrastructure.LanguageAssessments.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="StartLanguageAssessmentSessionCommand"/>.
/// </summary>
public class StartLanguageAssessmentSessionCommandHandler(
    IMapper mapper,
    ITimeProvider timeProvider,
    IQuizAssessmentSessionService assessmentSessionService,
    IGrammarAssessmentService grammarAssessmentService,
    IListeningAssessmentService listeningAssessmentService,
    IEventBusBroker eventBusBroker)
    : ICommandHandler<StartLanguageAssessmentSessionCommand, QuizAssessmentSessionDto>
{
    public async Task<QuizAssessmentSessionDto> Handle(StartLanguageAssessmentSessionCommand request, CancellationToken cancellationToken)
    {
        // Check for existing sessions
        var tenMinutesAgo = timeProvider.GetUtcNow().AddMinutes(-10);
        var twoHoursAgo = timeProvider.GetUtcNow().AddHours(-2);
        var existingSession = await assessmentSessionService.Get(s => 
                (
                    (s.State == AssessmentState.NotStarted && s.StartTime >= tenMinutesAgo) ||
                    (s.State == AssessmentState.InProgress && s.StartTime >= twoHoursAgo)
                ) &&
                s.ParticipantIpAddress == request.IpAddress, new QueryOptions(QueryTrackingMode.AsNoTracking))
            .Include(session => session.Assessments)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingSession != null)
            return mapper.Map<QuizAssessmentSessionDto>(existingSession);
        
        var session = new QuizAssessmentSession
        {
            StartTime = timeProvider.GetUtcNow(),
            State = AssessmentState.NotStarted,
            Step = LanguageAssessmentTypes.Grammar.ToString(),
            ParticipantIpAddress = request.IpAddress
        };

        // Create session
        var createdSession = await assessmentSessionService.CreateAsync(session, cancellationToken: cancellationToken);
        var assessments = new List<QuizAssessment>
        {
            new GrammarAssessment
            {
                Name = LanguageAssessmentTypes.Grammar.GetDescription(),
                Type = LanguageAssessmentTypes.Grammar.ToString(),
                Topics = ["english", "grammar"],
                Status = GenerationStatuses.NotStarted,
                Level = AssessmentLevel.Intermediate,
                SessionId = createdSession.Id
            },
            new ListeningAssessment
            {
                Name = LanguageAssessmentTypes.Listening.GetDescription(),
                Type = LanguageAssessmentTypes.Listening.ToString(),
                Topics = ["english", "listening"],
                Status = GenerationStatuses.NotStarted,
                Level = AssessmentLevel.Intermediate,
                AudioContent = "Initial Content",
                SessionId = createdSession.Id
            }
        };

        // Save assessments
        var skipSavingChanges = new CommandOptions(skipSavingChanges: true);
        var createdAssessments = await Task.WhenAll(assessments
            .Select(async (assessment, index) =>
            {
                var commandOptions = index != assessments.Count - 1 ? skipSavingChanges : default;
                var createdAssessment = null as QuizAssessment;

                if (assessment is GrammarAssessment grammarAssessment)
                    createdAssessment = await grammarAssessmentService.CreateAsync(grammarAssessment, commandOptions, cancellationToken);

                if (assessment is ListeningAssessment listeningAssessment)
                    createdAssessment = await listeningAssessmentService.CreateAsync(listeningAssessment, commandOptions, cancellationToken);

                return createdAssessment!;
            }));

        createdSession.Assessments = createdAssessments.ToList();

        // Publish event to generate language assessments
        await eventBusBroker.PublishAsync(
            new GenerateLanguageAssessmentEvent(createdAssessments.Select(assessment => (assessment.Id, assessment.Type)).ToList()),
            new EventOptions
            {
                Exchange = EventBusConstants.LanguageAssessmentExchangeName,
                RoutingKey = EventBusConstants.LanguageAssessmentGenerationQueueName,
                IsPersistent = true
            },
            cancellationToken);

        return mapper.Map<QuizAssessmentSessionDto>(createdSession);
    }
}