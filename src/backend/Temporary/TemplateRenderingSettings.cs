namespace Temporary;

/// <summary>
/// Represents a settings for rendering templates
/// </summary>
public sealed record TemplateRenderingSettings
{
    /// <summary>
    /// Gets the regex pattern for placeholder in template.
    /// </summary>
    public string PlaceholderRegexPattern { get; init; } = default!;

    /// <summary>
    /// Gets the regex pattern for value placeholder. 
    /// </summary>
    public string PlaceholderValueRegexPattern { get; init; } = default!;

    /// <summary>
    /// Gets the regex match timeout in seconds.
    /// </summary>
    public int RegexMatchTimeoutInSeconds { get; init; }
}