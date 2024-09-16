using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents quiz question.
/// </summary>
public sealed record  QuizQuestion : Question
{
    /// <summary>
    /// Gets or sets the type of the question.
    /// </summary>
    public QuestionType Type { get; set; }

    /// <summary>
    /// Gets or sets the ID of the associated QuizAssessment.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Navigation property for the associated assessment.
    /// </summary>
    public QuizAssessment QuizAssessment { get; set; } = default!;

    /// <summary>
    /// Navigation property for the associated answer.
    /// </summary>
    public List<QuizOption> Answers { get; set; } = new();
}