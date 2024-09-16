using Backbone.DataAccess.Relational.Entities.Models;
using Backbone.Storage.Cache.Abstractions.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents an abstract assessment.
/// </summary>
public abstract record Assessment : IPrimaryEntity, ICacheEntry
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string CacheKey => Id.ToString();

    /// <summary>
    /// Gets or sets the assessment level.
    /// </summary>
    public AssessmentLevel Level { get; set; }
}