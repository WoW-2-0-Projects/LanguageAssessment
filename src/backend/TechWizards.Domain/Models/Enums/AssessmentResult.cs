namespace TechWizards.Domain.Models.Enums;

/// <summary>
/// Defines assessment results 
/// </summary>
public enum AssessmentResults : byte
{
    /// <summary>
    /// Refers to a correct answer.
    /// </summary>
    Correct,
    
    /// <summary>
    /// Refers to a wrong answer.
    /// </summary>
    Incorrect,
    
    /// <summary>
    /// Refers to a skipped question.
    /// </summary>
    Skipped
}