using Backbone.DataAccess.Relational.Entities.Events;
using FluentValidation;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.QuizOptions.Validators;

/// <summary>
/// Provides validation rules for QuizOption entities.
/// </summary>
public class QuizOptionValidator : AbstractValidator<QuizOption>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuizOptionValidator"/> class.
    /// </summary>
    public QuizOptionValidator()
    {
        RuleSet(EntityEventType.OnCreate.ToString(), () =>
        {
            ApplyCommonRules();
        });

        RuleSet(EntityEventType.OnUpdate.ToString(), () =>
        {
            ApplyCommonRules();
        });
    }

    private void ApplyCommonRules()
    {
        RuleFor(o => o.OptionText)
            .NotEmpty().WithMessage("Option text is required.")
            .MaximumLength(500).WithMessage("Option text must not exceed 500 characters.");

        RuleFor(o => o.QuestionId)
            .NotEmpty().WithMessage("QuizQuestion ID is required.");
    }
}