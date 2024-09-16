using AutoMapper;
using Backbone.Comms.Infra.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.QuizAssessmentSessions.Models;
using TechWizards.Application.QuizAssessmentSessions.Queries;
using TechWizards.Application.QuizAssessmentSessions.Services;

namespace TechWizards.Infrastructure.QuizAssessmentSessions.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="QuizAssessmentSessionResultGetByIdQuery"/>.
/// </summary>
public class QuizAssessmentSessionResultGetByIdQueryHandler(IMapper mapper, IQuizAssessmentSessionService assessmentSessionService)
    : IQueryHandler<QuizAssessmentSessionResultGetByIdQuery, QuizAssessmentSessionResultDto?>
{
    public async Task<QuizAssessmentSessionResultDto?> Handle(QuizAssessmentSessionResultGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundSession = await assessmentSessionService
            .Get(qas => qas.Id == request.Id,
                new QueryOptions(QueryTrackingMode.AsNoTracking))
            .Include(s => s.Assessments)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<QuizAssessmentSessionResultDto>(foundSession);
    }
}