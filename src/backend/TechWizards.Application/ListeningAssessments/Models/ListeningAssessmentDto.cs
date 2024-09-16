using TechWizards.Application.QuizQuestions.Models;

namespace TechWizards.Application.ListeningAssessments.Models;

/// <summary>
/// Represents a data transfer object for listening assessment.
/// </summary>
public class ListeningAssessmentDto
{
    /// <summary>
    /// Gets the grammar assessment ID.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Navigation property for the associated quiz questions.
    /// </summary>
    public ICollection<QuizQuestionDto> Questions { get; init; } = default!;
    
    /// <summary>
    /// Gets or sets the session id.
    /// </summary>
    public Guid SessionId { get; init; }

    /// <summary>
    /// Gets or sets the audio file URL.
    /// </summary>
    public string AudioFileUrl { get; init; } = default!;
}