using Backbone.DataAccess.Relational.Entities.Models;
using Backbone.Storage.Cache.Abstractions.Models;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents a quiz option for a quiz question.
/// </summary>
public sealed record  QuizOption : IPrimaryEntity, ICacheEntry
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string CacheKey => Id.ToString();

    /// <summary>
    /// Gets or sets the text of the option.
    /// </summary>
    public string OptionText { get; set; } = default!;

    /// <summary>
    /// Gets or sets a value indicating whether this option is correct.
    /// </summary>
    public bool IsCorrect { get; set; }

    /// <summary>
    /// Gets or sets the ID of the associated quiz question.
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// Navigation property for the associated QuizQuestion.
    /// </summary>
    public QuizQuestion QuizQuestion { get; set; } = default!;
}