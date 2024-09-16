using TechWizards.Application.QuizAssessments.Models;

namespace TechWizards.Application.QuizAssessmentSessions.Models;

/// <summary>
/// Represents overall results for the session.
/// </summary>
public class QuizAssessmentSessionResultDto
{
    /// <summary>
    /// Gets assessment session ID.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the assessments for this session.
    /// </summary>
    public ICollection<QuizAssessmentResultDto> Assessments { get; init; } = default!;
}