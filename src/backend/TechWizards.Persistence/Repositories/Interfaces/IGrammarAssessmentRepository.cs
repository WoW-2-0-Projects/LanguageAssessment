using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines grammar assessment repository functionality.
/// </summary>
public interface IGrammarAssessmentRepository
{
    IQueryable<GrammarAssessment> Get(
        Expression<Func<GrammarAssessment, bool>>? predicate = default,
        QueryOptions queryOptions = default
    );

    ValueTask<GrammarAssessment?> GetByIdAsync(
        Guid grammarAssessmentId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<bool> CheckAsync<TValue>(
        IQueryable<TValue> queryableSource, TValue? expectedValue = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<GrammarAssessment> CreateAsync(
        GrammarAssessment grammarAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<GrammarAssessment> UpdateAsync(
        GrammarAssessment grammarAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<GrammarAssessment?> DeleteByIdAsync(
        Guid grammarAssessmentId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    ValueTask<GrammarAssessment?> DeleteAsync(
        GrammarAssessment grammarAssessment,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
}