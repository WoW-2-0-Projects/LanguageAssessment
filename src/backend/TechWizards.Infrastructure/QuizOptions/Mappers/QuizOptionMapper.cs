using AutoMapper;
using TechWizards.Application.QuizOptions.Models;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.QuizOptions.Mappers;

/// <summary>
/// Provides mapping configuration for QuizOption entities and DTOs.
/// </summary>
public class QuizOptionMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuizOptionMapper"/> class.
    /// </summary>
    public QuizOptionMapper()
    {
        CreateMap<QuizOption, QuizOptionDto>().ReverseMap();
    }
}