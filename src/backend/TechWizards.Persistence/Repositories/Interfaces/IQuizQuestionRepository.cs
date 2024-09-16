using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines the quiz question repository functionality.
/// </summary>
public interface IQuizQuestionRepository
{
    /// <summary>
    /// Retrieves a queryable collection of QuizQuestions based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the QuizQuestions.</param>
    /// <param name="queryOptions">Optional query options for sorting, paging, etc.</param>
    /// <returns>An IQueryable of QuizQuestions.</returns>
    IQueryable<QuizQuestion> Get(Expression<Func<QuizQuestion, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a QuizQuestion by its unique identifier.
    /// </summary>
    /// <param name="quizQuestionId">The unique identifier of the QuizQuestion.</param>
    /// <param name="queryOptions">Optional query options for tracking, etc.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The QuizQuestion if found, otherwise null.</returns>
    ValueTask<QuizQuestion?> GetByIdAsync(Guid quizQuestionId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a QuizQuestion exists based on the provided queryable source.
    /// </summary>
    /// <typeparam name="TValue">The type of the value being checked.</typeparam>
    /// <param name="queryableSource">The queryable source to check against.</param>
    /// <param name="expectedValue">The expected value to compare with.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>True if the QuizQuestion exists, otherwise false.</returns>
    ValueTask<bool> CheckAsync<TValue>(IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new QuizQuestion.
    /// </summary>
    /// <param name="quizQuestion">The QuizQuestion to create.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The created QuizQuestion.</returns>
    ValueTask<QuizQuestion> CreateAsync(QuizQuestion quizQuestion, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing QuizQuestion.
    /// </summary>
    /// <param name="quizQuestion">The QuizQuestion to update.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The updated QuizQuestion.</returns>
    ValueTask<QuizQuestion> UpdateAsync(QuizQuestion quizQuestion, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a QuizQuestion by its unique identifier.
    /// </summary>
    /// <param name="quizQuestionId">The unique identifier of the QuizQuestion to delete.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The deleted QuizQuestion if found, otherwise null.</returns>
    ValueTask<QuizQuestion?> DeleteByIdAsync(Guid quizQuestionId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specific QuizQuestion.
    /// </summary>
    /// <param name="quizQuestion">The QuizQuestion to delete.</param>
    /// <param name="commandOptions">Optional command options.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The deleted QuizQuestion if found, otherwise null.</returns>
    ValueTask<QuizQuestion?> DeleteAsync(QuizQuestion quizQuestion, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}