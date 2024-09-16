using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.DataAccess.Relational.Entities.Events;
using Backbone.General.Validations.Abstractions.Exceptions;
using Backbone.Storage.Cache.Abstractions.Brokers;
using FluentValidation;
using TechWizards.Application.GrammarAssessments.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Persistence.Extensions;
using TechWizards.Persistence.Repositories.Interfaces;

namespace TechWizards.Infrastructure.GrammarAssessments.Services;

/// <summary>
/// Provides grammar assessment foundation service functionality.
/// </summary>
public class GrammarAssessmentService(
    ICacheStorageBroker cacheStorageBroker,
    IGrammarAssessmentRepository grammarAssessmentRepository,
    IValidator<GrammarAssessment>? grammarAssessmentValidator = null
) : IGrammarAssessmentService
{
    public IQueryable<GrammarAssessment> Get(
        Expression<Func<GrammarAssessment, bool>>? predicate = default,
        QueryOptions queryOptions = default
    )
    {
        return grammarAssessmentRepository.Get(predicate, queryOptions);
    }

    public ValueTask<GrammarAssessment?> GetByIdAsync(
        Guid grammarAssessmentId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return grammarAssessmentRepository.GetByIdAsync(grammarAssessmentId, queryOptions, cancellationToken);
    }

    public ValueTask<bool> CheckByIdAsync(Guid grammarAssessmentId, CancellationToken cancellationToken = default)
    {
        var assessmentExistsCheck = cacheStorageBroker.GetOrSetAsync(CacheKeyExtensions.CheckById<GrammarAssessment>(grammarAssessmentId), async ct =>
        {
            return await grammarAssessmentRepository
                .CheckAsync(Get(assessment => assessment.Id == grammarAssessmentId)
                    .Select(user => (Guid?)user.Id), cancellationToken: ct);
        }, cancellationToken: cancellationToken);

        return assessmentExistsCheck;
    }

    public async ValueTask<GrammarAssessment> CreateAsync(
        GrammarAssessment grammarAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        if (grammarAssessmentValidator is not null)
        {
            var validationResult = await grammarAssessmentValidator.ValidateAsync(
                grammarAssessment,
                options => options.IncludeRuleSets(EntityEventType.OnCreate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }

        return await grammarAssessmentRepository.CreateAsync(grammarAssessment, commandOptions, cancellationToken);
    }

    public async ValueTask<GrammarAssessment> UpdateAsync(
        GrammarAssessment grammarAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        if (grammarAssessmentValidator is not null)
        {
            var validationResult = await grammarAssessmentValidator.ValidateAsync(
                grammarAssessment,
                options => options.IncludeRuleSets(EntityEventType.OnUpdate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }

        return await grammarAssessmentRepository.UpdateAsync(grammarAssessment, commandOptions, cancellationToken);
    }

    public ValueTask<GrammarAssessment?> DeleteByIdAsync(
        Guid grammarAssessmentId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return grammarAssessmentRepository.DeleteByIdAsync(grammarAssessmentId, commandOptions, cancellationToken);
    }

    public ValueTask<GrammarAssessment?> DeleteAsync(
        GrammarAssessment grammarAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return grammarAssessmentRepository.DeleteAsync(grammarAssessment, commandOptions, cancellationToken);
    }
}