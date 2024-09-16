namespace TechWizards.Infrastructure.GrammarAssessments.Settings;

/// <summary>
/// Represents the settings for generating a grammar assessment.
/// </summary>
public sealed record GrammarAssessmentGenerationSettings
{
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