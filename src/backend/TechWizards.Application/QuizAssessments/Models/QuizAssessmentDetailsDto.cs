using TechWizards.Domain.Models.Enums;

namespace TechWizards.Application.QuizAssessments.Models;

/// <summary>
/// Represents the data transfer object (DTO) for quiz assessment.
/// </summary>
public sealed record QuizAssessmentDetailsDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the quiz assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the status of the quiz assessment.
    /// </summary>
    public GenerationStatuses Status { get; init; }

    /// <summary>
    /// Gets the assessment type.
    /// </summary>
    public LanguageAssessmentTypes Type { get; set; }
}