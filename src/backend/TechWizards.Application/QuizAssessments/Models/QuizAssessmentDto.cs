using TechWizards.Application.QuizQuestions.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Application.QuizAssessments.Models;

/// <summary>
/// Represents the data transfer object (DTO) for quiz assessment.
/// </summary>
public class QuizAssessmentDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the quiz assessment.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the assessment level.
    /// </summary>
    public AssessmentLevel Level { get; set; }

    /// <summary>
    /// Gets or sets the topics of the quiz assessment.
    /// </summary>
    public ICollection<string> Topics { get; init; } = new List<string>();

    /// <summary>
    /// Gets the questions of the quiz assessment.
    /// </summary>
    public ICollection<QuizQuestionDto>? Questions { get; init; }
}