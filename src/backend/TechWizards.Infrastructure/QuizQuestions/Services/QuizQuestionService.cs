using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.DataAccess.Relational.Entities.Events;
using Backbone.General.Validations.Abstractions.Exceptions;
using Backbone.Storage.Cache.Abstractions.Brokers;
using FluentValidation;
using TechWizards.Application.QuizQuestions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Persistence.Extensions;
using TechWizards.Persistence.Repositories.Interfaces;

namespace TechWizards.Infrastructure.QuizQuestions.Services;

/// <summary>
/// Provides quiz question foundation service functionality.
/// </summary>
public class QuizQuestionService(
    ICacheStorageBroker cacheStorageBroker,
    IQuizQuestionRepository quizQuestionRepository,
    IValidator<QuizQuestion>? quizQuestionValidator = null
) : IQuizQuestionService
{
    public IQueryable<QuizQuestion> Get(
        Expression<Func<QuizQuestion, bool>>? predicate = default,
        QueryOptions queryOptions = default
    )
    {
        return quizQuestionRepository.Get(predicate, queryOptions);
    }

    public ValueTask<QuizQuestion?> GetByIdAsync(
        Guid quizQuestionId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return quizQuestionRepository.GetByIdAsync(quizQuestionId, queryOptions, cancellationToken);
    }

    public ValueTask<bool> CheckByIdAsync(Guid quizQuestionId, CancellationToken cancellationToken = default)
    {
        var questionExistsCheck = cacheStorageBroker.GetOrSetAsync(CacheKeyExtensions.CheckById<QuizQuestion>(quizQuestionId), async ct =>
        {
            return await quizQuestionRepository
                .CheckAsync(Get(question => question.Id == quizQuestionId)
                    .Select(user => (Guid?)user.Id), cancellationToken: ct);
        }, cancellationToken: cancellationToken);

        return questionExistsCheck;
    }

    public async ValueTask<QuizQuestion> CreateAsync(
        QuizQuestion quizQuestion,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        if (quizQuestionValidator is not null)
        {
            var validationResult = await quizQuestionValidator.ValidateAsync(
                quizQuestion,
                options => options.IncludeRuleSets(EntityEventType.OnCreate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }
            
        return await quizQuestionRepository.CreateAsync(quizQuestion, commandOptions, cancellationToken);
    }

    public async ValueTask<QuizQuestion> UpdateAsync(
        QuizQuestion quizQuestion,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        if (quizQuestionValidator is not null)
        {
            var validationResult = await quizQuestionValidator.ValidateAsync(
                quizQuestion,
                options => options.IncludeRuleSets(EntityEventType.OnUpdate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }
            
        return await quizQuestionRepository.UpdateAsync(quizQuestion, commandOptions, cancellationToken);
    }

    public ValueTask<QuizQuestion?> DeleteByIdAsync(
        Guid quizQuestionId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return quizQuestionRepository.DeleteByIdAsync(quizQuestionId, commandOptions, cancellationToken);
    }

    public ValueTask<QuizQuestion?> DeleteAsync(
        QuizQuestion quizQuestion,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return quizQuestionRepository.DeleteAsync(quizQuestion, commandOptions, cancellationToken);
    }
}