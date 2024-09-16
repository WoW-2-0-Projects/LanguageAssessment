using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines the repository operations for QuizOption entities.
/// </summary>
public interface IQuizOptionRepository
{
    /// <summary>
    /// Retrieves a queryable collection of QuizOptions based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the QuizOptions.</param>
    /// <param name="queryOptions">Optional query options for sorting, paging, etc.</param>
    /// <returns>An IQueryable of QuizOptions.</returns>
    IQueryable<QuizOption> Get(Expression<Func<QuizOption, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a QuizOption by its unique identifier.
    /// </summary>
    /// <param name="quizOptionId">The unique identifier of the QuizOption.</param>
    /// <param name="queryOptions">Optional query options for tracking, etc.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The QuizOption if found, otherwise null.</returns>
    ValueTask<QuizOption?> GetByIdAsync(Guid quizOptionId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a QuizOption exists based on the provided queryable source.
    /// </summary>
    /// <typeparam name="TValue">The type of the value being checked.</typeparam>
    /// <param name="queryableSource">The queryable source to check against.</param>
    /// <param name="expectedValue">The expected value to compare with.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>True if the QuizOption exists, otherwise false.</returns>
    ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new QuizOption.
    /// </summary>
    /// <param name="quizOption">The QuizOption to create.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The created QuizOption.</returns>
    ValueTask<QuizOption> CreateAsync(QuizOption quizOption, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing QuizOption.
    /// </summary>
    /// <param name="quizOption">The QuizOption to update.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The updated QuizOption.</returns>
    ValueTask<QuizOption> UpdateAsync(QuizOption quizOption, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a QuizOption by its unique identifier.
    /// </summary>
    /// <param name="quizOptionId">The unique identifier of the QuizOption to delete.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The deleted QuizOption if found, otherwise null.</returns>
    ValueTask<QuizOption?> DeleteByIdAsync(Guid quizOptionId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specific QuizOption.
    /// </summary>
    /// <param name="quizOption">The QuizOption to delete.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The deleted QuizOption if found, otherwise null.</returns>
    ValueTask<QuizOption?> DeleteAsync(QuizOption quizOption, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}