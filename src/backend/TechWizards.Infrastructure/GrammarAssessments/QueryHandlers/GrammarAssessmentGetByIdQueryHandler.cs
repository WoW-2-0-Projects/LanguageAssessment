using AutoMapper;
using Backbone.Comms.Infra.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.GrammarAssessments.Models;
using TechWizards.Application.GrammarAssessments.Queries;
using TechWizards.Application.GrammarAssessments.Services;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Infrastructure.GrammarAssessments.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="GrammarAssessmentGetByIdQuery"/>.
/// </summary>
public class GrammarAssessmentGetByIdQueryHandler(IMapper mapper, IGrammarAssessmentService quizAssessmentService)
    : IQueryHandler<GrammarAssessmentGetByIdQuery, GrammarAssessmentDto?>
{
    public async Task<GrammarAssessmentDto?> Handle(GrammarAssessmentGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundGrammarAssessment = await quizAssessmentService
            .Get(qa => qa.Id == request.Id && qa.Type == LanguageAssessmentTypes.Grammar.ToString(),
                new QueryOptions(QueryTrackingMode.AsNoTracking))
            .Include(qa => qa.Questions)
            .ThenInclude(qq => qq.Answers)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<GrammarAssessmentDto>(foundGrammarAssessment);
    }
}