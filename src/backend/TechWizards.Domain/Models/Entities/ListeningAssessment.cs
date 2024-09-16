using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents listening assessment.
/// </summary>
public sealed record ListeningAssessment : QuizAssessment
{
    public ListeningAssessment()
    {
        Type = LanguageAssessmentTypes.Listening.ToString();
    }
    
    /// <summary>
    /// Gets or sets listening audio content.
    /// </summary>
    public string AudioContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the audio file URL.
    /// </summary>
    public string AudioFileUrl { get; set; }
}