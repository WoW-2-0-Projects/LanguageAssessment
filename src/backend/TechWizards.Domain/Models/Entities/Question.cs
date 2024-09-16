using Backbone.DataAccess.Relational.Entities.Models;
using Backbone.Storage.Cache.Abstractions.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents an abstract question.
/// </summary>
public abstract record Question : IPrimaryEntity, ICacheEntry
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string CacheKey => Id.ToString();

    /// <summary>
    /// Gets or sets the text of the question.
    /// </summary>
    public string QuestionText { get; set; } = default!;
}