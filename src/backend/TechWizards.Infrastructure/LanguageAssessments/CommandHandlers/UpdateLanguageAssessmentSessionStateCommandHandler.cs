using AutoMapper;
using Backbone.Comms.Infra.Abstractions.Commands;
using TechWizards.Application.LanguageAssessments.Commands;
using TechWizards.Application.QuizAssessmentSessions.Services;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Infrastructure.LanguageAssessments.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="UpdateLanguageAssessmentSessionStateCommand"/>.
/// </summary>
public class UpdateLanguageAssessmentSessionStateCommandHandler(IQuizAssessmentSessionService sessionService, IMapper mapper)
    : ICommandHandler<UpdateLanguageAssessmentSessionStateCommand>
{
    public async Task Handle(UpdateLanguageAssessmentSessionStateCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetByIdAsync(request.SessionId, cancellationToken: cancellationToken)
                      ?? throw new InvalidOperationException($"Session with ID {request.SessionId} not found.");

        if (!IsValidStateTransition(session.State, request.NewState))
            throw new InvalidOperationException($"Invalid state transition from {session.State} to {request.NewState}");

        session.State = request.NewState;
        await sessionService.UpdateAsync(session, cancellationToken: cancellationToken);
    }

    private bool IsValidStateTransition(AssessmentState currentState, AssessmentState newState)
    {
        return (currentState, newState) switch
        {
            (AssessmentState.NotStarted, AssessmentState.InProgress) => true,
            (AssessmentState.NotStarted, AssessmentState.Canceled) => true,
            (AssessmentState.InProgress, AssessmentState.Completed) => true,
            (AssessmentState.InProgress, AssessmentState.Canceled) => true,
            _ => false
        };
    }
}