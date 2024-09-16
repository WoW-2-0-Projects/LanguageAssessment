using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.DataAccess.Relational.EfCore.Repositories.Cached.Repositories;
using Backbone.Storage.Cache.Abstractions.Brokers;
using TechWizards.Domain.Models.Entities;
using TechWizards.Persistence.DataContexts;
using TechWizards.Persistence.Repositories.Interfaces;

namespace TechWizards.Persistence.Repositories;

/// <summary>
/// Provides listening assessment repository functionality.
/// </summary>
public class ListeningAssessmentRepository(AppDbContext dbContext, ICacheStorageBroker cacheBroker)
    : CachedPrimaryEntityRepositoryBase<ListeningAssessment, AppDbContext>(dbContext, cacheBroker), IListeningAssessmentRepository
{
    public new IQueryable<ListeningAssessment> Get(Expression<Func<ListeningAssessment, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<ListeningAssessment?> GetByIdAsync(Guid listeningAssessmentId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(listeningAssessmentId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue, cancellationToken);
    }

    public new ValueTask<ListeningAssessment> CreateAsync(ListeningAssessment listeningAssessment, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(listeningAssessment, commandOptions, cancellationToken);
    }

    public new ValueTask<ListeningAssessment> UpdateAsync(ListeningAssessment listeningAssessment, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(listeningAssessment, commandOptions, cancellationToken);
    }

    public new ValueTask<ListeningAssessment?> DeleteByIdAsync(Guid listeningAssessmentId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(listeningAssessmentId, commandOptions, cancellationToken);
    }

    public new ValueTask<ListeningAssessment?> DeleteAsync(ListeningAssessment listeningAssessment, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(listeningAssessment, commandOptions, cancellationToken);
    }
}