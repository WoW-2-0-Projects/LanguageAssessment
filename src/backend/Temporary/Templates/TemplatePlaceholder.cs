﻿namespace Temporary.Templates;

/// <summary>
/// Represents a placeholder for template
/// </summary>
public class TemplatePlaceholder
{
    /// <summary>
    /// Gets or sets placeholder of the value 
    /// </summary>
    public string Placeholder { get; set; } = default!;

    /// <summary>
    /// Gets or sets value of placeholder
    /// </summary>
    public string PlaceholderValue { get; set; } = default!;

    /// <summary>
    /// Gets or sets value of the template
    /// </summary>
    public string? Value { get; set; } = default!;

    /// <summary>
    /// Gets or sets if the placeholder is valid or not
    /// </summary>
    public bool IsValid { get; set; }
}
