using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.DataAccess.Relational.Entities.Events;
using Backbone.General.Validations.Abstractions.Exceptions;
using Backbone.Storage.Cache.Abstractions.Brokers;
using FluentValidation;
using TechWizards.Application.QuizOptions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Persistence.Extensions;
using TechWizards.Persistence.Repositories.Interfaces;

namespace TechWizards.Infrastructure.QuizOptions.Services;

/// <summary>
/// Provides quiz question foundation service functionality.
/// </summary>
public class QuizOptionService(
    ICacheStorageBroker cacheStorageBroker,
    IQuizOptionRepository quizOptionRepository,
    IValidator<QuizOption>? quizQuestionValidator = null
) : IQuizOptionService
{
    public IQueryable<QuizOption> Get(
        Expression<Func<QuizOption, bool>>? predicate = default,
        QueryOptions queryOptions = default)
    {
        return quizOptionRepository.Get(predicate, queryOptions);
    }

    public ValueTask<QuizOption?> GetByIdAsync(
        Guid quizOptionId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return quizOptionRepository.GetByIdAsync(quizOptionId, queryOptions, cancellationToken);
    }

    public ValueTask<bool> CheckByIdAsync(Guid quizOptionId, CancellationToken cancellationToken = default)
    {
        var optionExistsCheck = cacheStorageBroker.GetOrSetAsync(
            CacheKeyExtensions.CheckById<QuizOption>(quizOptionId),
            async ct =>
            {
                return await quizOptionRepository
                    .CheckAsync(Get(option => option.Id == quizOptionId)
                            .Select(option => (Guid?)option.Id),
                        cancellationToken: ct);
            },
            cancellationToken: cancellationToken);

        return optionExistsCheck;
    }

    public async ValueTask<QuizOption> CreateAsync(
        QuizOption quizOption,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (quizQuestionValidator is not null)
        {
            var validationResult = await quizQuestionValidator.ValidateAsync(
                quizOption,
                options => options.IncludeRuleSets(EntityEventType.OnCreate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }

        return await quizOptionRepository.CreateAsync(quizOption, commandOptions, cancellationToken);
    }

    public async ValueTask<QuizOption> UpdateAsync(
        QuizOption quizOption,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        if (quizQuestionValidator is not null)
        {
            var validationResult = await quizQuestionValidator.ValidateAsync(
                quizOption,
                options => options.IncludeRuleSets(EntityEventType.OnUpdate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }

        return await quizOptionRepository.UpdateAsync(quizOption, commandOptions, cancellationToken);
    }

    public ValueTask<QuizOption?> DeleteByIdAsync(
        Guid quizOptionId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return quizOptionRepository.DeleteByIdAsync(quizOptionId, commandOptions, cancellationToken);
    }

    public ValueTask<QuizOption?> DeleteAsync(
        QuizOption quizOption,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return quizOptionRepository.DeleteAsync(quizOption, commandOptions, cancellationToken);
    }
}