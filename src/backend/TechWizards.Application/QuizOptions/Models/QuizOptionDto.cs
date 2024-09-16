namespace TechWizards.Application.QuizOptions.Models;

/// <summary>
/// Represents a data transfer object for a quiz option.
/// </summary>
public class QuizOptionDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the quiz option.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the text of the option.
    /// </summary>
    public string OptionText { get; set; } = default!;
}