using AutoMapper;
using TechWizards.Application.GrammarAssessments.Models;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.GrammarAssessments.Mappers;

/// <summary>
/// Mapper of quiz assessment models.
/// </summary>
public class GrammarAssessmentMapper : Profile
{
    public GrammarAssessmentMapper()
    {
        CreateMap<GrammarAssessment, GrammarAssessmentDto>().ReverseMap();
    }
}