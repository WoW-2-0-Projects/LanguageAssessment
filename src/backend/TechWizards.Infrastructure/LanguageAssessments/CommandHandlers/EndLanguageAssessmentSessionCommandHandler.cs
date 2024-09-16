using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.General.Time.Provider.Brokers;
using TechWizards.Application.LanguageAssessments.Commands;
using TechWizards.Application.QuizAssessmentSessions.Services;
using TechWizards.Domain.Models.Enums;
using ArgumentException = System.ArgumentException;

namespace TechWizards.Infrastructure.LanguageAssessments.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="StartLanguageAssessmentSessionCommand"/>.
/// </summary>
public class EndLanguageAssessmentSessionCommandHandler(
    ITimeProvider timeProvider,
    IQuizAssessmentSessionService sessionService
) : ICommandHandler<EndLanguageAssessmentSessionCommand>
{
    public async Task Handle(EndLanguageAssessmentSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetByIdAsync(request.SessionId, cancellationToken: cancellationToken);

        if (session == null)
            throw new ArgumentException($"Session with ID {request.SessionId} not found.");

        if (session.State == AssessmentState.Completed)
            throw new InvalidOperationException("Session is already completed.");

        // Update session properties
        session = session with
        {
            EndTime = timeProvider.GetUtcNow(),
            State = AssessmentState.Completed
        };

        await sessionService.UpdateAsync(session, cancellationToken: cancellationToken);
    }
}