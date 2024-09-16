using AutoMapper;
using TechWizards.Application.ListeningAssessments.Models;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.ListeningAssessments.Mappers;

/// <summary>
/// Mapper of quiz assessment models.
/// </summary>
public class ListeningAssessmentMapper : Profile
{
    public ListeningAssessmentMapper()
    {
        CreateMap<ListeningAssessment, ListeningAssessmentDto>().ReverseMap();
    }
}