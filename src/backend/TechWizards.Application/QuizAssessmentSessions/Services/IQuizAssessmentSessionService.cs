using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Application.QuizAssessmentSessions.Services;

/// <summary>
/// Defines foundation service for quiz assessment sessions
/// </summary>
public interface IQuizAssessmentSessionService
{
    /// <summary>
    /// Gets a queryable source of quiz assessment sessions based on an optional predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter quiz assessment sessions.</param>
    /// <param name="queryOptions">Query options.</param>
    /// <returns>Queryable source of quiz assessment sessions.</returns>
    IQueryable<QuizAssessmentSession> Get(
        Expression<Func<QuizAssessmentSession, bool>>? predicate = default,
        QueryOptions queryOptions = default
    );

    /// <summary>
    /// Gets a quiz assessment session by its ID.
    /// </summary>
    /// <param name="sessionId">The ID of the quiz assessment session.</param>
    /// <param name="queryOptions">Query options for sorting, paging, etc.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>QuizAssessmentSession if found, otherwise null.</returns>
    ValueTask<QuizAssessmentSession?> GetByIdAsync(
        Guid sessionId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Checks if a quiz assessment session exists by its ID
    /// </summary>
    /// <param name="sessionId">The ID of quiz assessment session to check</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>True if a quiz assessment session with the given ID exists, otherwise false</returns>
    ValueTask<bool> CheckByIdAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a new quiz assessment session.
    /// </summary>
    /// <param name="session">The quiz assessment session to be created.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Created quiz assessment session.</returns>
    ValueTask<QuizAssessmentSession> CreateAsync(
        QuizAssessmentSession session,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates an existing quiz assessment session.
    /// </summary>
    /// <param name="session">The quiz assessment session to be updated.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Updated quiz assessment session.</returns>
    ValueTask<QuizAssessmentSession> UpdateAsync(
        QuizAssessmentSession session,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a quiz assessment session by its ID.
    /// </summary>
    /// <param name="sessionId">The ID of the quiz assessment session to be deleted.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Deleted quiz assessment session if soft deleted, otherwise null.</returns>
    ValueTask<QuizAssessmentSession?> DeleteByIdAsync(
        Guid sessionId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a quiz assessment session.
    /// </summary>
    /// <param name="session">The quiz assessment session to be deleted.</param>
    /// <param name="commandOptions">Command options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Deleted quiz assessment session if soft deleted, otherwise null.</returns>
    ValueTask<QuizAssessmentSession?> DeleteAsync(
        QuizAssessmentSession session,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
}