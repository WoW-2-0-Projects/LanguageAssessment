using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Application.ListeningAssessments.Services;

/// <summary>
/// Defines foundation service for listening assessments.
/// </summary>
public interface IListeningAssessmentService
{
    IQueryable<ListeningAssessment> Get(
        Expression<Func<ListeningAssessment, bool>>? predicate = default,
        QueryOptions queryOptions = default
    );

    ValueTask<ListeningAssessment?> GetByIdAsync(
        Guid listeningAssessmentId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<bool> CheckByIdAsync(
        Guid listeningAssessmentId,
        CancellationToken cancellationToken = default
    );

    ValueTask<ListeningAssessment> CreateAsync(
        ListeningAssessment listeningAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<ListeningAssessment> UpdateAsync(
        ListeningAssessment listeningAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<ListeningAssessment?> DeleteByIdAsync(
        Guid listeningAssessmentId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<ListeningAssessment?> DeleteAsync(
        ListeningAssessment listeningAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
}