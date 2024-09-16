using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Application.GrammarAssessments.Models;

namespace TechWizards.Application.GrammarAssessments.Queries;

/// <summary>
/// Represents a query for retrieving a grammar assessment by its ID.
/// </summary>
public record GrammarAssessmentGetByIdQuery : IQuery<GrammarAssessmentDto?>
{
    /// <summary>
    /// Gets or sets the grammar assessment ID.
    /// </summary>
    public Guid Id { get; init; }
}