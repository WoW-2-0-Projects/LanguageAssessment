using Backbone.DataAccess.Relational.Entities.Models;
using Backbone.Storage.Cache.Abstractions.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents a user submitted answer.
/// </summary>
public sealed record QuizAnswer : IPrimaryEntity, ICacheEntry
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string CacheKey => Id.ToString();

    /// <summary>
    /// Gets or sets the ID of the associated quiz question.
    /// </summary>
    public Guid QuestionId { get; init; }

    /// <summary>
    /// Gets or sets the ID of the submitted option.
    /// </summary>
    public Guid? SubmittedOptionId { get; init; }

    /// <summary>
    /// Gets or sets the ID of the correct option.
    /// </summary>
    public Guid CorrectOptionId { get; init; }

    /// <summary>
    /// Gets or sets the result of the assessment.
    /// </summary>
    public AssessmentResults Result { get; init; }

    /// <summary>
    /// Navigation property for the submitted option.
    /// </summary>
    public QuizOption? SubmittedOption { get; init; }

    /// <summary>
    /// Navigation property for the correct option.
    /// </summary>
    public QuizOption CorrectOption { get; init; } = default!;

    /// <summary>
    /// Navigation property for the associated quiz question.
    /// </summary>
    public QuizQuestion QuizQuestion { get; init; } = default!;
}