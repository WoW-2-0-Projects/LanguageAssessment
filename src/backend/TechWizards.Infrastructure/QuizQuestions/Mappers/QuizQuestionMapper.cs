using AutoMapper;
using TechWizards.Application.QuizQuestions.Models;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.QuizQuestions.Mappers;

/// <summary>
/// Mapper of quiz question models.
/// </summary>
public class QuizQuestionMapper : Profile
{
    public QuizQuestionMapper()
    {
        CreateMap<QuizQuestion, QuizQuestionDto>().ReverseMap();
    }
}