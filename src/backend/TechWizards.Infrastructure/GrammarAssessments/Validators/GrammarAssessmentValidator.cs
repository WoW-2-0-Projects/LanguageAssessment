using Backbone.DataAccess.Relational.Entities.Events;
using FluentValidation;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;
using TechWizards.Infrastructure.LanguageAssessments.Constants;
using TechWizards.Infrastructure.QuizAssessments.Validators;

namespace TechWizards.Infrastructure.GrammarAssessments.Validators;

/// <summary>
/// Validator for the GrammarAssessment entity.
/// </summary>
public class GrammarAssessmentValidator : QuizAssessmentValidator<GrammarAssessment>
{
    public GrammarAssessmentValidator()
    {
        RuleSet(EntityEventType.OnCreate.ToString(), () =>
        {
            ApplyCommonRules();
            ApplyGrammarSpecificRules();
        });

        RuleSet(EntityEventType.OnUpdate.ToString(), () =>
        {
            ApplyCommonRules();
            ApplyGrammarSpecificRules();
        });
    }

    private void ApplyGrammarSpecificRules()
    {
        RuleFor(assessment => assessment.Type)
            .Equal(LanguageAssessmentTypes.Grammar.ToString())
            .WithMessage("Assessment type must be Grammar for GrammarAssessment.");
    }
}