using TechWizards.Application.QuizOptions.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Application.QuizQuestions.Models;

/// <summary>
/// Represents the data transfer object (DTO) for quiz questions.
/// </summary>
public class QuizQuestionDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the quiz question.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the text of the quiz question.
    /// </summary>
    public string QuestionText { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the quiz question.
    /// </summary>
    public QuestionType Type { get; set; }
    
    /// <summary>
    /// Gets the questions of the quiz assessment.
    /// </summary>
    public ICollection<QuizOptionDto>? Answers { get; init; }
}