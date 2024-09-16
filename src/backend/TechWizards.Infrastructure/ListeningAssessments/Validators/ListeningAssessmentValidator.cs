using Backbone.DataAccess.Relational.Entities.Events;
using FluentValidation;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;
using TechWizards.Infrastructure.LanguageAssessments.Constants;
using TechWizards.Infrastructure.QuizAssessments.Validators;

namespace TechWizards.Infrastructure.ListeningAssessments.Validators;

/// <summary>
/// Validator for the ListeningAssessment entity.
/// </summary>
public class ListeningAssessmentValidator : QuizAssessmentValidator<ListeningAssessment>
{
    public ListeningAssessmentValidator()
    {
        RuleSet(EntityEventType.OnCreate.ToString(), () =>
        {
            ApplyCommonRules();
            ApplyListeningSpecificRules();
        });

        RuleSet(EntityEventType.OnUpdate.ToString(), () =>
        {
            ApplyCommonRules();
            ApplyListeningSpecificRules();
        });
    }

    private void ApplyListeningSpecificRules()
    {
        RuleFor(assessment => assessment.Type)
            .Equal(LanguageAssessmentTypes.Listening.ToString())
            .WithMessage("Assessment type must be Listening for ListeningAssessment.");

        RuleFor(assessment => assessment.AudioContent)
            .NotEmpty().WithMessage("Audio content is required for listening assessment.");
    }
}