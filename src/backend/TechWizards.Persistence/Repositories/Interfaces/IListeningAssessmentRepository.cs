using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines listening assessment repository functionality.
/// </summary>
public interface IListeningAssessmentRepository
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

    ValueTask<bool> CheckAsync<TValue>(
        IQueryable<TValue> queryableSource, TValue? expectedValue = default,
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