using AutoMapper;
using Backbone.Comms.Infra.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.GrammarAssessments.Queries;
using TechWizards.Application.ListeningAssessments.Models;
using TechWizards.Application.ListeningAssessments.Queries;
using TechWizards.Application.ListeningAssessments.Services;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Infrastructure.ListeningAssessments.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="GrammarAssessmentGetByIdQuery"/>.
/// </summary>
public class ListingAssessmentGetByIdQueryHandler(IMapper mapper, IListeningAssessmentService quizAssessmentService)
    : IQueryHandler<ListeningAssessmentGetByIdQuery, ListeningAssessmentDto?>
{
    public async Task<ListeningAssessmentDto?> Handle(ListeningAssessmentGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundGrammarAssessment = await quizAssessmentService
            .Get(qa => qa.Id == request.Id && qa.Type == LanguageAssessmentTypes.Listening.ToString(),
                new QueryOptions(QueryTrackingMode.AsNoTracking))
            .Include(qa => qa.Questions)
            .ThenInclude(qq => qq.Answers)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<ListeningAssessmentDto>(foundGrammarAssessment);
    }
}