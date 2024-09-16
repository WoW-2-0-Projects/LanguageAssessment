using AutoMapper;
using Backbone.Comms.Infra.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.QuizAssessmentSessions.Models;
using TechWizards.Application.QuizAssessmentSessions.Queries;
using TechWizards.Application.QuizAssessmentSessions.Services;

namespace TechWizards.Infrastructure.QuizAssessmentSessions.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="QuizAssessmentSessionGetByIdQuery"/>.
/// </summary>
public class QuizAssessmentSessionGetByIdQueryHandler(IMapper mapper, IQuizAssessmentSessionService assessmentSessionService)
    : IQueryHandler<QuizAssessmentSessionGetByIdQuery, QuizAssessmentSessionDto?>
{
    public async Task<QuizAssessmentSessionDto?> Handle(QuizAssessmentSessionGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundSession = await assessmentSessionService
            .Get(qas => qas.Id == request.Id,
                new QueryOptions(QueryTrackingMode.AsNoTracking))
            .Include(s => s.Assessments)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<QuizAssessmentSessionDto>(foundSession);
    }
}