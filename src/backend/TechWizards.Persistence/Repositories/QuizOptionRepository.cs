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
/// Implements the repository operations for QuizOption entities.
/// </summary>
public class QuizOptionRepository(AppDbContext dbContext, ICacheStorageBroker cacheBroker) : 
    CachedPrimaryEntityRepositoryBase<QuizOption, AppDbContext>(dbContext, cacheBroker), IQuizOptionRepository
{
    /// <inheritdoc />
    public new IQueryable<QuizOption> Get(Expression<Func<QuizOption, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    /// <inheritdoc />
    public new ValueTask<QuizOption?> GetByIdAsync(Guid quizOptionId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(quizOptionId, queryOptions, cancellationToken);
    }

    /// <inheritdoc />
    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default, CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue, cancellationToken);
    }

    /// <inheritdoc />
    public new ValueTask<QuizOption> CreateAsync(QuizOption quizOption, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(quizOption, commandOptions, cancellationToken);
    }

    /// <inheritdoc />
    public new ValueTask<QuizOption> UpdateAsync(QuizOption quizOption, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(quizOption, commandOptions, cancellationToken);
    }

    /// <inheritdoc />
    public new ValueTask<QuizOption?> DeleteByIdAsync(Guid quizOptionId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(quizOptionId, commandOptions, cancellationToken);
    }

    /// <inheritdoc />
    public new ValueTask<QuizOption?> DeleteAsync(QuizOption quizOption, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(quizOption, commandOptions, cancellationToken);
    }
}