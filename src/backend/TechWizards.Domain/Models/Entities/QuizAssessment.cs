using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents quiz assessment.
/// </summary>
public abstract record QuizAssessment : Assessment
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the topics.
    /// </summary>
    public List<string> Topics { get; set; } = new();

    /// <summary>
    /// Gets or sets the status of the generation.
    /// </summary>
    public GenerationStatuses Status { get; set; }

    /// <summary>
    /// Navigation property for the associated quiz questions.
    /// </summary>
    public List<QuizQuestion> Questions { get; set; } = new();
    
    /// <summary>
    /// Gets or sets the state of the session.
    /// </summary>
    public AssessmentState State { get; set; }
    
    /// <summary>
    /// Gets or sets the score.
    /// </summary>
    public uint Score { get; set; }

    /// <summary>
    /// Gets or sets the session id.
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// Navigation property for the associated session.
    /// </summary>
    public QuizAssessmentSession Session { get; set; } = default!;

    /// <summary>
    /// Navigation property for the associated participant answers.
    /// </summary>
    public ICollection<QuizAnswer> ParticipantAnswers { get; set; } = default!;
}