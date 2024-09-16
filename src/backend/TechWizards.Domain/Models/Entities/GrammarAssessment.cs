using TechWizards.Domain.Models.Enums;

namespace TechWizards.Domain.Models.Entities;

/// <summary>
/// Represents grammar assessment.
/// </summary>
public sealed record GrammarAssessment : QuizAssessment
{
    public GrammarAssessment()
    {
        Type = LanguageAssessmentTypes.Grammar.ToString();
    }
}