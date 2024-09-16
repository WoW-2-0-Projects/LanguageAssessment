using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Backbone.DataAccess.Relational.Entities.Events;
using Backbone.General.Validations.Abstractions.Exceptions;
using Backbone.Storage.Cache.Abstractions.Brokers;
using FluentValidation;
using TechWizards.Application.ListeningAssessments.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Persistence.Extensions;
using TechWizards.Persistence.Repositories;
using TechWizards.Persistence.Repositories.Interfaces;

namespace TechWizards.Infrastructure.ListeningAssessments.Services;

/// <summary>
/// Provides listening assessment foundation service functionality.
/// </summary>
public class ListeningAssessmentService(
    ICacheStorageBroker cacheStorageBroker,
    IListeningAssessmentRepository listeningAssessmentRepository,
    IValidator<ListeningAssessment>? listeningAssessmentValidator = null
) : IListeningAssessmentService
{
    public IQueryable<ListeningAssessment> Get(
        Expression<Func<ListeningAssessment, bool>>? predicate = default,
        QueryOptions queryOptions = default
    )
    {
        return listeningAssessmentRepository.Get(predicate, queryOptions);
    }

    public ValueTask<ListeningAssessment?> GetByIdAsync(
        Guid listeningAssessmentId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return listeningAssessmentRepository.GetByIdAsync(listeningAssessmentId, queryOptions, cancellationToken);
    }

    public ValueTask<bool> CheckByIdAsync(Guid listeningAssessmentId, CancellationToken cancellationToken = default)
    {
        var assessmentExistsCheck = cacheStorageBroker.GetOrSetAsync(CacheKeyExtensions.CheckById<ListeningAssessment>(listeningAssessmentId), async ct =>
        {
            return await listeningAssessmentRepository
                .CheckAsync(Get(assessment => assessment.Id == listeningAssessmentId)
                    .Select(user => (Guid?)user.Id), cancellationToken: ct);
        }, cancellationToken: cancellationToken);

        return assessmentExistsCheck;
    }

    public async ValueTask<ListeningAssessment> CreateAsync(
        ListeningAssessment listeningAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        if (listeningAssessmentValidator is not null)
        {
            var validationResult = await listeningAssessmentValidator.ValidateAsync(
                listeningAssessment,
                options => options.IncludeRuleSets(EntityEventType.OnCreate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }
            
        return await listeningAssessmentRepository.CreateAsync(listeningAssessment, commandOptions, cancellationToken);
    }

    public async ValueTask<ListeningAssessment> UpdateAsync(
        ListeningAssessment listeningAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        if (listeningAssessmentValidator is not null)
        {
            var validationResult = await listeningAssessmentValidator.ValidateAsync(
                listeningAssessment,
                options => options.IncludeRuleSets(EntityEventType.OnUpdate.ToString()),
                cancellationToken
            );
            if (!validationResult.IsValid)
                throw new AppValidationException(new ValidationException(validationResult.Errors));
        }
            
        return await listeningAssessmentRepository.UpdateAsync(listeningAssessment, commandOptions, cancellationToken);
    }

    public ValueTask<ListeningAssessment?> DeleteByIdAsync(
        Guid listeningAssessmentId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return listeningAssessmentRepository.DeleteByIdAsync(listeningAssessmentId, commandOptions, cancellationToken);
    }

    public ValueTask<ListeningAssessment?> DeleteAsync(
        ListeningAssessment listeningAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return listeningAssessmentRepository.DeleteAsync(listeningAssessment, commandOptions, cancellationToken);
    }
}