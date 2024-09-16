namespace TechWizards.Infrastructure.ListeningAssessments.Settings;

/// <summary>
/// Represents the settings for generating a grammar assessment.
/// </summary>
public sealed record ListeningAssessmentGenerationSettings
{
    /// <summary>
    /// Gets the minimum number of words to generate for listening text.
    /// </summary>
    public uint MinNumberOfWords { get; init; }
    
    /// <summary>
    /// Gets the maximum number of words to generate for listening text.
    /// </summary>
    public uint MaxNumberOfWords { get; init; }
    
    /// <summary>
    /// Gets the required number of questions to generate.
    /// </summary>
    public uint NumberOfQuestions { get; init; }

    /// <summary>
    /// Gets the required number of answers to generate.
    /// </summary>
    public uint NumberOfAnswers { get; init; }

    /// <summary>
    /// Gets the default topics to use if none are specified.
    /// </summary>
    public ICollection<string> DefaultTopics { get; init; } = default!;
}