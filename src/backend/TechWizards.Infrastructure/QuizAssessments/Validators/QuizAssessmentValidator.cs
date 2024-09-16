using Backbone.DataAccess.Relational.Entities.Events;
using TechWizards.Domain.Models.Entities;
using FluentValidation;

namespace TechWizards.Infrastructure.QuizAssessments.Validators;

/// <summary>
/// Validator for the QuizAssessment entity.
/// </summary>
public abstract class QuizAssessmentValidator<T> : AbstractValidator<T> where T : QuizAssessment
{
    public QuizAssessmentValidator()
    {
        RuleSet(EntityEventType.OnCreate.ToString(), ApplyCommonRules);

        RuleSet(EntityEventType.OnUpdate.ToString(), ApplyCommonRules);
    }

    protected virtual void ApplyCommonRules()
    {
        RuleFor(assessment => assessment.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 256).WithMessage("Name must be between 3 and 256 characters.");

        RuleFor(assessment => assessment.Topics)
            .NotNull().WithMessage("Topics cannot be null.")
            .Must(topics => topics.Count > 0).WithMessage("At least one topic is required.");

        RuleForEach(assessment => assessment.Topics)
            .NotEmpty().WithMessage("Topic cannot be empty.")
            .Must(topic => !string.IsNullOrWhiteSpace(topic)).WithMessage("Topic cannot be whitespace.")
            .Length(3, 256).WithMessage("Each topic must be between 3 and 256 characters.");
    }
}