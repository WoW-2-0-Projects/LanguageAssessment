using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.DataAccess.Relational.Entities.Events;
using Backbone.General.Validations.Abstractions.Exceptions;
using Backbone.Storage.Cache.Abstractions.Brokers;
using FluentValidation;
using TechWizards.Application.QuizAssessmentSessions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Persistence.Extensions;
using TechWizards.Persistence.Repositories.Interfaces;

namespace TechWizards.Infrastructure.QuizAssessmentSessions.Services;

/// <summary>
/// Provides quiz assessment session foundation service functionality
/// </summary>
public class QuizAssessmentSessionService(
    ICacheStorageBroker cacheStorageBroker,
    IQuizAssessmentSessionRepository quizAssessmentSessionRepository,
    IValidator<QuizAssessmentSession>? quizAssessmentSessionValidator = null)
    : IQuizAssessmentSessionService
{
    public IQueryable<QuizAssessmentSession> Get(
        Expression<Func<QuizAssessmentSession, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return quizAssessmentSessionRepository.Get(predicate, queryOptions);
    }

    public ValueTask<QuizAssessmentSession?> GetByIdAsync(
        Guid sessionId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return quizAssessmentSessionRepository.GetByIdAsync(sessionId, queryOptions, cancellationToken);
    }

    public ValueTask<bool> CheckByIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        var sessionExistsCheck = cacheStorageBroker.GetOrSetAsync(
            CacheKeyExtensions.CheckById<QuizAssessmentSession>(sessionId), 
            async ct =>
            {
                return await quizAssessmentSessionRepository.CheckAsync(
                    Get(session => session.Id == sessionId).Select(session => (Guid?)session.Id),
                    cancellationToken: ct);
            }, 
            cancellationToken: cancellationToken);

        return sessionExistsCheck;
    }

    public async ValueTask<QuizAssessmentSession> CreateAsync(
        QuizAssessmentSession session,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (quizAssessmentSessionValidator is not null)
        {
            var validationResult = await quizAssessmentSessionValidator.ValidateAsync(
                session,
                options => options.IncludeRuleSets(EntityEventType.OnCreate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }
            
        return await quizAssessmentSessionRepository.CreateAsync(session, commandOptions, cancellationToken);
    }

    public async ValueTask<QuizAssessmentSession> UpdateAsync(
        QuizAssessmentSession session,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (quizAssessmentSessionValidator is not null)
        {
            var validationResult = await quizAssessmentSessionValidator.ValidateAsync(
                session,
                options => options.IncludeRuleSets(EntityEventType.OnUpdate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }
            
        return await quizAssessmentSessionRepository.UpdateAsync(session, commandOptions, cancellationToken);
    }

    public ValueTask<QuizAssessmentSession?> DeleteByIdAsync(
        Guid sessionId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return quizAssessmentSessionRepository.DeleteByIdAsync(sessionId, commandOptions, cancellationToken);
    }

    public ValueTask<QuizAssessmentSession?> DeleteAsync(
        QuizAssessmentSession session,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return quizAssessmentSessionRepository.DeleteAsync(session, commandOptions, cancellationToken);
    }
}