using Backbone.DataAccess.Relational.Entities.Models;
using Backbone.Storage.Cache.Abstractions.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents an abstract assessment session.
/// </summary>
public abstract record AssessmentSession : IPrimaryEntity, ICacheEntry
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string CacheKey => Id.ToString();

    /// <summary>
    /// Gets the client IP address.
    /// </summary>
    public string ParticipantIpAddress { get; init; } = default!;
    
    /// <summary>
    /// Gets or initializes the start time of the session.
    /// </summary>
    public DateTimeOffset StartTime { get; init; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the state of the session.
    /// </summary>
    public AssessmentState State { get; set; }

    /// <summary>
    /// Gets or sets the end time of the session.
    /// </summary>
    public DateTimeOffset? EndTime { get; set; }
}