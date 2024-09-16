using AutoMapper;
using TechWizards.Application.QuizAssessmentSessions.Models;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.QuizAssessmentSessions.Mappers;

public class QuizAssessmentSessionMapper : Profile
{
    public QuizAssessmentSessionMapper()
    {
        CreateMap<QuizAssessmentSession, QuizAssessmentSessionDto>();
        CreateMap<QuizAssessmentSession, QuizAssessmentSessionResultDto>().ReverseMap();
    }
}