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
/// Provides grammar assessment repository functionality.
/// </summary>
public class GrammarAssessmentRepository(AppDbContext dbContext, ICacheStorageBroker cacheBroker)
    : CachedPrimaryEntityRepositoryBase<GrammarAssessment, AppDbContext>(dbContext, cacheBroker), IGrammarAssessmentRepository
{
    public new IQueryable<GrammarAssessment> Get(Expression<Func<GrammarAssessment, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<GrammarAssessment?> GetByIdAsync(Guid grammarAssessmentId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(grammarAssessmentId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue, cancellationToken);
    }

    public new ValueTask<GrammarAssessment> CreateAsync(GrammarAssessment grammarAssessment, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(grammarAssessment, commandOptions, cancellationToken);
    }

    public new ValueTask<GrammarAssessment> UpdateAsync(GrammarAssessment grammarAssessment, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(grammarAssessment, commandOptions, cancellationToken);
    }

    public new ValueTask<GrammarAssessment?> DeleteByIdAsync(Guid grammarAssessmentId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(grammarAssessmentId, commandOptions, cancellationToken);
    }

    public new ValueTask<GrammarAssessment?> DeleteAsync(GrammarAssessment grammarAssessment, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(grammarAssessment, commandOptions, cancellationToken);
    }
}