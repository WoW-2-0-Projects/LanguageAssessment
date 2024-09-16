using Backbone.Comms.Infra.Abstractions.Commands;

namespace TechWizards.Application.ListeningAssessments.Commands;

/// <summary>
/// Represents a command to submit a listing assessment.
/// </summary>
public class SubmitListeningAssessmentCommand : ICommand
{
    /// <summary>
    /// Gets the ID of the assessment.
    /// </summary>
    public Guid AssessmentId { get; init; }

    /// <summary>
    /// Gets the participant answers.
    /// </summary>
    public Dictionary<Guid, Guid> Answers { get; init; } = default!;
}