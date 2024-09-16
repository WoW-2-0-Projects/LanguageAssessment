using TechWizards.Application.QuizAssessments.Models;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Application.QuizAssessmentSessions.Models;

/// <summary>
/// Represents abstract data transfer object for assessment session.
/// </summary>
public sealed record QuizAssessmentSessionDto
{
    /// <summary>
    /// Gets assessment session ID.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Gets or sets the state of the session.
    /// </summary>
    public AssessmentState State { get; init; }

    /// <summary>
    /// Gets the assessment generation statuses.
    /// </summary>
    public ICollection<QuizAssessmentDetailsDto> Assessments { get; init; } = default!;
}