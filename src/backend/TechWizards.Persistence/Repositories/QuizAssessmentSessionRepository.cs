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
/// Provides quiz assessment session repository functionality.
/// </summary>
public class QuizAssessmentSessionRepository(AppDbContext dbContext, ICacheStorageBroker cacheBroker)
    : CachedPrimaryEntityRepositoryBase<QuizAssessmentSession, AppDbContext>(dbContext, cacheBroker), IQuizAssessmentSessionRepository
{
    public new IQueryable<QuizAssessmentSession> Get(Expression<Func<QuizAssessmentSession, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<QuizAssessmentSession?> GetByIdAsync(Guid sessionId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(sessionId, queryOptions, cancellationToken);
    }

    public new ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default)
    {
        return base.CheckAsync(queryableSource, expectedValue, cancellationToken);
    }

    public new ValueTask<QuizAssessmentSession> CreateAsync(QuizAssessmentSession session, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(session, commandOptions, cancellationToken);
    }

    public new ValueTask<QuizAssessmentSession> UpdateAsync(QuizAssessmentSession session, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(session, commandOptions, cancellationToken);
    }

    public new ValueTask<QuizAssessmentSession?> DeleteByIdAsync(Guid sessionId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(sessionId, commandOptions, cancellationToken);
    }

    public new ValueTask<QuizAssessmentSession?> DeleteAsync(QuizAssessmentSession session, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(session, commandOptions, cancellationToken);
    }
}