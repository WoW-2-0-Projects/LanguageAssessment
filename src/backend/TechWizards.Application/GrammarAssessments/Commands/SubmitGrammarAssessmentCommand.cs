using Backbone.Comms.Infra.Abstractions.Commands;

namespace TechWizards.Application.GrammarAssessments.Commands;

/// <summary>
/// Represents a command to submit a grammar assessment.
/// </summary>
public class SubmitGrammarAssessmentCommand : ICommand
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