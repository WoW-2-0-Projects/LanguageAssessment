using Backbone.Comms.Infra.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.GrammarAssessments.Commands;
using TechWizards.Application.GrammarAssessments.Services;
using TechWizards.Application.QuizAssessmentSessions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Infrastructure.GrammarAssessments.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="SubmitGrammarAssessmentCommand"/>.
/// </summary>
public class SubmitGrammarAssessmentCommandHandler(
    IQuizAssessmentSessionService sessionService,
    IGrammarAssessmentService grammarAssessmentService)
    : ICommandHandler<SubmitGrammarAssessmentCommand>
{
    public async Task Handle(SubmitGrammarAssessmentCommand request, CancellationToken cancellationToken)
    {
        var assessment = await grammarAssessmentService
            .Get(a => a.Id == request.AssessmentId)
            .Include(a => a.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(cancellationToken);

        if (assessment == null)
            throw new InvalidOperationException($"Assessment with ID {request.AssessmentId} not found.");

        if (assessment.State == AssessmentState.Completed)
            throw new InvalidOperationException("Assessment is already completed.");

        if (assessment.Questions.Count != request.Answers.Count)
            throw new InvalidOperationException("The number of submitted answers does not match the number of questions in the assessment.");

        var invalidAnswers = request.Answers.Any(answer =>
        {
            // Check if question exists
            if (!assessment.Questions.Exists(assessmentQuestion => assessmentQuestion.Id == answer.Key))
                return true;

            // Check if answer exists
            if (!assessment.Questions
                    .First(assessmentQuestion => assessmentQuestion.Id == answer.Key).Answers
                    .Exists(assessmentAnswer => assessmentAnswer.Id == answer.Value))
                return true;

            return false;
        });

        if (invalidAnswers)
            throw new InvalidOperationException($"Invalid questions or answers are submitted");

        // Check chosen options
        var answers = assessment.Questions.Select(question =>
        {
            var submittedAnswer = request.Answers.FirstOrDefault(answer => answer.Key == question.Id);
            var correctAnswer = question.Answers.First(a => a.IsCorrect);

            var result = submittedAnswer.Value == Guid.Empty
                ? AssessmentResults.Skipped
                : correctAnswer.Id == submittedAnswer.Value
                    ? AssessmentResults.Correct
                    : AssessmentResults.Incorrect;

            return new QuizAnswer
            {
                QuestionId = question.Id,
                SubmittedOptionId = submittedAnswer.Value != Guid.Empty ? submittedAnswer.Value : null,
                CorrectOptionId = correctAnswer.Id,
                Result = result
            };
        });

        var correctAnswers = answers.Count(a => a.Result == AssessmentResults.Correct);
        assessment.Score = (uint)((decimal)correctAnswers / assessment.Questions.Count * 100);
        assessment.State = AssessmentState.Completed;
        await grammarAssessmentService.UpdateAsync(assessment, cancellationToken: cancellationToken);

        // Check if all assessments in the session are completed
        var session = await sessionService
            .Get(session => session.Id == assessment.SessionId)
            .Include(session => session.Assessments)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException($"Session with ID {assessment.SessionId} not found.");

        if (session.State != AssessmentState.InProgress)
        {
            session.State = AssessmentState.InProgress;
            await sessionService.UpdateAsync(session, cancellationToken: cancellationToken);
        }
        
        // Check if all other assessments ar finished
        if (session.Assessments.TrueForAll(a => a.State == AssessmentState.Completed))
        {
            session.State = AssessmentState.Completed;
            await sessionService.UpdateAsync(session, cancellationToken: cancellationToken);
        }
    }
}