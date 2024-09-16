using Backbone.DataAccess.Relational.Entities.Events;
using Backbone.General.Time.Provider.Brokers;
using FluentValidation;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Infrastructure.QuizAssessmentSessions.Validators;

/// <summary>
/// Validator for the quiz assessment session entity.
/// </summary>
public class QuizAssessmentSessionValidator : AbstractValidator<QuizAssessmentSession>
{
    public QuizAssessmentSessionValidator(ITimeProvider timeProvider)
    {
        RuleSet(nameof(EntityEventType.OnCreate), () =>
        {
            RuleFor(session => session.StartTime)
                .NotEmpty().WithMessage("Start time is required.");
                // TODO : Fix validation .LessThanOrEqualTo(timeProvider.GetUtcNow()).WithMessage("Start time cannot be in the future.");

            RuleFor(session => session.EndTime)
                .Null().WithMessage("End time should be null when creating a session.");

            RuleFor(session => session.Step)
                .NotEmpty().WithMessage("Assessment step is required.")
                .Length(3, 256)
                .WithMessage("Assessment step must be between 3 and 256 characters.");
        });

        RuleSet(nameof(EntityEventType.OnUpdate), () =>
        {
            RuleFor(session => session.StartTime)
                .NotEmpty().WithMessage("Start time is required.")
                .LessThanOrEqualTo(timeProvider.GetUtcNow()).WithMessage("Start time cannot be in the future.");

            RuleFor(session => session.EndTime)
                .GreaterThan(session => session.StartTime).When(session => session.EndTime.HasValue)
                .WithMessage("End time must be after the start time.");

            RuleFor(session => session.Step)
                .NotEmpty().WithMessage("Assessment step is required.")
                .Length(3, 256)
                .WithMessage("Assessment step must be between 3 and 256 characters.");
        });
    }
}