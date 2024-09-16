using System.Linq.Expressions;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Application.GrammarAssessments.Services;

/// <summary>
/// Defines foundation service for grammar assessments.
/// </summary>
public interface IGrammarAssessmentService
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

    ValueTask<bool> CheckByIdAsync(
        Guid grammarAssessmentId,
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