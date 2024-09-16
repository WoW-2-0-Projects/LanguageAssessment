using FluentValidation.Results;
using Temporary.Templates;

namespace Temporary.TextTemplates.Services;

/// <summary>
/// Defines the template rendering foundation service functionality.
/// </summary>
public interface ITextTemplateRenderingService
{
    /// <summary>
    /// Renders template with given variables
    /// </summary>
    /// <param name="template">The template with placeholders to render</param>
    /// <param name="variables">A dictionary of variables to replace placeholders</param>
    /// <returns>Rendered template content.</returns>
    string Render(string template, Dictionary<string, string>? variables = default);

    /// <summary>
    /// Validates placeholders in the template
    /// </summary>
    /// <param name="templatePlaceholders">A collection of template placeholders</param>
    /// <returns>The validation result</returns>
    ValidationResult ValidatePlaceholders(IEnumerable<TemplatePlaceholder> templatePlaceholders);
}