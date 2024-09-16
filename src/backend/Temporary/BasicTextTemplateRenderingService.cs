using System.Text;
using System.Text.RegularExpressions;
using Backbone.General.Validations.Abstractions.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Temporary.Templates;
using Temporary.TextTemplates.Services;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Temporary;

/// <summary>
/// Provides the foundation service functionality for rendering templates.
/// </summary>
public class BasicTextTemplateRenderingService(IOptions<TemplateRenderingSettings> templateRenderingSettingsOptions) : ITextTemplateRenderingService
{
    private readonly TemplateRenderingSettings _templateRenderingSettings = templateRenderingSettingsOptions.Value;

    public string Render(string template, Dictionary<string, string>? variables = default)
    {
        if (string.IsNullOrWhiteSpace(template))
            return template;

        // Prepare regex for placeholder and variables extraction
        var placeholderRegex = new Regex(_templateRenderingSettings.PlaceholderRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var placeholderValueRegex = new Regex(_templateRenderingSettings.PlaceholderValueRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var matches = placeholderRegex.Matches(template);

        if (matches.Any() && (variables is null || !variables.Any()))
            throw new AppValidationException(
                new ValidationException([new ValidationFailure(nameof(variables), "Variables are required for this template.")]), false);

        // Extract placeholders and their values
        var templatePlaceholders = matches.Select(match =>
            {
                var placeholder = match.Value;
                var placeholderValue = placeholderValueRegex.Match(placeholder).Groups[1].Value;
                var valid = variables!.TryGetValue(placeholderValue, out var value);

                return new TemplatePlaceholder
                {
                    Placeholder = placeholder,
                    PlaceholderValue = placeholderValue,
                    Value = value,
                    IsValid = valid
                };
            })
            .ToList();

        // Validate placeholders
        var placeholdersValidationResult = ValidatePlaceholders(templatePlaceholders);
        if (!placeholdersValidationResult.IsValid)
            throw new AppValidationException(new ValidationException(placeholdersValidationResult.Errors), false);

        // Replace placeholders with their values
        var messageBuilder = new StringBuilder(template);
        templatePlaceholders.ForEach(placeholder => messageBuilder.Replace(placeholder.Placeholder, placeholder.Value));

        return messageBuilder.ToString();
    }

    public ValidationResult ValidatePlaceholders(IEnumerable<TemplatePlaceholder> templatePlaceholders)
    {
        var validationErrors = templatePlaceholders
            .Where(placeholder => !placeholder.IsValid)
            .Select(placeholder =>
            {
                var errorMessage = $"Variable for placeholder '{placeholder.PlaceholderValue}' is not found";
                return new ValidationFailure(placeholder.PlaceholderValue, errorMessage);
            }).ToList();

        return new ValidationResult(validationErrors);
    }
}