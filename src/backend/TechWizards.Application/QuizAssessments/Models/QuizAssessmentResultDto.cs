namespace TechWizards.Application.QuizAssessments.Models;

/// <summary>
/// Represents the data transfer object for assessment result.
/// </summary>
public class QuizAssessmentResultDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the quiz assessment.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the score.
    /// </summary>
    public uint Score { get; set; }
}