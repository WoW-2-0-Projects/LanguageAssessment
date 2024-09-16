using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Application.ListeningAssessments.Models;
using TechWizards.Application.QuizAssessments.Models;

namespace TechWizards.Application.ListeningAssessments.Queries;

/// <summary>
/// Represents a query for retrieving a listening assessment by its ID.
/// </summary>
public record ListeningAssessmentGetByIdQuery : IQuery<ListeningAssessmentDto?>
{
    /// <summary>
    /// Gets or sets the listening assessment ID.
    /// </summary>
    public Guid Id { get; init; }
}