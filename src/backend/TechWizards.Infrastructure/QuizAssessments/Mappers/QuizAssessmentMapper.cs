using AutoMapper;
using TechWizards.Application.QuizAssessments.Models;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.QuizAssessments.Mappers;

public class QuizAssessmentMapper : Profile
{
    public QuizAssessmentMapper()
    {
        CreateMap<QuizAssessment, QuizAssessmentDetailsDto>().ReverseMap();
        CreateMap<QuizAssessment, QuizAssessmentResultDto>();
    }
}