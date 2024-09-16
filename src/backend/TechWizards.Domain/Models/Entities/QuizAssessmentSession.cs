namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents a session for assessment.
/// </summary>
public sealed record QuizAssessmentSession : AssessmentSession
{
    /// <summary>
    /// Gets or sets the current assessment step. 
    /// </summary>
    public string Step { get; set; } = default!;

    /// <summary>
    /// Navigation property for the associated quiz assessment.
    /// </summary>
    public List<QuizAssessment> Assessments { get; set; } = default!;
}