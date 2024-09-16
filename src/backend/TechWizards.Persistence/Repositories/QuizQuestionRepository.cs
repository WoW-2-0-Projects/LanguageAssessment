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
/// Provides quiz question repository functionality.
/// </summary>
public class QuizQuestionRepository(AppDbContext dbContext, ICacheStorageBroker cacheBroker)
    : CachedPrimaryEntityRepositoryBase<QuizQuestion, AppDbContext>(dbContext, cacheBroker), IQuizQuestionRepository
{
    public new IQueryable<QuizQuestion> Get(Expression<Func<QuizQuestion, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<QuizQuestion?> GetByIdAsync(Guid quizQuestionId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(quizQuestionId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue, cancellationToken);
    }

    public new ValueTask<QuizQuestion> CreateAsync(QuizQuestion quizQuestion, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(quizQuestion, commandOptions, cancellationToken);
    }

    public new ValueTask<QuizQuestion> UpdateAsync(QuizQuestion quizQuestion, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(quizQuestion, commandOptions, cancellationToken);
    }

    public new ValueTask<QuizQuestion?> DeleteByIdAsync(Guid quizQuestionId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(quizQuestionId, commandOptions, cancellationToken);
    }

    public new ValueTask<QuizQuestion?> DeleteAsync(QuizQuestion quizQuestion, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(quizQuestion, commandOptions, cancellationToken);
    }
}