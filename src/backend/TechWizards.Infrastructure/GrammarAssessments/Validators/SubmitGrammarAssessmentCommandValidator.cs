using FluentValidation;
using TechWizards.Application.GrammarAssessments.Commands;

namespace TechWizards.Infrastructure.GrammarAssessments.Validators;

public class SubmitGrammarAssessmentCommandValidator : AbstractValidator<SubmitGrammarAssessmentCommand>
{
    public SubmitGrammarAssessmentCommandValidator()
    {
        RuleFor(x => x.AssessmentId)
            .NotEmpty().WithMessage("Assessment ID is required.");

        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage("Answers are required.")
            .Must(answers => answers.All(a => a.Key != Guid.Empty && a.Value != Guid.Empty))
            .WithMessage("All question and answer IDs must be valid GUIDs.");
    }
}