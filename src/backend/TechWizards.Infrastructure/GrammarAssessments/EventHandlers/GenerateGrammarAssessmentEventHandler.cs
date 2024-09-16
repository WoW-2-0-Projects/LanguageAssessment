using Backbone.AiCapabilities.NaturalLanguageProcessing.ChatCompletion.Abstractions.Services.Interfaces;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.General.Serialization.Json.Abstractions.Brokers;
using Backbone.General.Validations.Abstractions.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using TechWizards.Application.GrammarAssessments.Events;
using TechWizards.Application.GrammarAssessments.Services;
using TechWizards.Application.QuizOptions.Services;
using TechWizards.Application.QuizQuestions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;
using TechWizards.Infrastructure.GrammarAssessments.Settings;
using TechWizards.Infrastructure.LanguageAssessments.Constants;
using Temporary.Extensions;
using Temporary.TextTemplates.Services;

namespace TechWizards.Infrastructure.GrammarAssessments.EventHandlers;

/// <summary>
/// Handles the execution of the <see cref="GenerateGrammarAssessmentEvent"/>.
/// </summary>
public class GenerateGrammarAssessmentEventHandler(
    ILogger<GenerateGrammarAssessmentEventHandler> logger,
    IOptions<GrammarAssessmentGenerationSettings> generationSettings,
    IServiceScopeFactory serviceScopeFactory,
    IJsonSerializer jsonSerializer,
    IChatCompletionService chatCompletionService,
    ITextTemplateRenderingService templateRenderingService,
    IGrammarAssessmentService grammarAssessmentService
) : EventHandlerBase<GenerateGrammarAssessmentEvent>
{
    protected override async ValueTask HandleAsync(GenerateGrammarAssessmentEvent eventContext, CancellationToken cancellationToken)
    {
        var generationSettingsValue = generationSettings.Value;

        var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var quizQuestionService = serviceProvider.GetRequiredService<IQuizQuestionService>();
        var quizOptionService = serviceProvider.GetRequiredService<IQuizOptionService>();

        var foundAssessment = await grammarAssessmentService.GetByIdAsync(eventContext.GrammarAssessmentId, cancellationToken: cancellationToken)
                              ?? throw new InvalidOperationException($"Grammar assessment with id {eventContext.GrammarAssessmentId} was not found.");

        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                5,
                retryAttempt => TimeSpan.FromSeconds(1),
                (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogError("Retry attempt {RetryCount} after {TimeSpanSeconds} seconds due to: {ExceptionMessage}", retryCount,
                        timeSpan.TotalSeconds, exception.Message);
                }
            );
        
        const string prompt =
            """
                ## Concepts
            
                Grammar assessment - english language assessment focusing on grammar rules and usage.
                Question - grammar related question
                Answer - right or wrong answer to a question, depending on requirements
            
                ## Instructions
            
                Generate a grammar assessment with the following requirements:
            
                ## Requirements :
            
                - question type must be one of - {{QuestionTypes}}
                - questions difficulty level - {{AssessmentLevel}}
                - total questions number must be exactly - {{NumberOfQuestions}}
                - total answers number must be exactly - {{NumberOfAnswers}}
                - single-choice questions must have all total answers that include only one correct answer
                - multi-choice questions must have all total answers that include more than one correct answer
                - questions topics - {{Topics}}
                - result format must be in JSON but without ticks
                - result format must strictly follow following JSON format :
            
                [
                    {
                        "Type": "QuestionType",
                        "QuestionText": "Question text here",
                        "Answers": [
                            {
                                "OptionText": "Option 1 text",
                                "IsCorrect": true
                            },
                            {
                                "OptionText": "Option 2 text",
                                "IsCorrect": false
                            }
                        ]
                    }
                ]
            """;

        var promptArguments = new Dictionary<string, string>
        {
            { PromptConstants.QuestionTypes, string.Join(',', EnumExtensions.GetAllDescriptions<QuestionType>()) },
            { PromptConstants.AssessmentLevel, foundAssessment.Level.ToString() },
            { PromptConstants.NumberOfQuestions, generationSettingsValue.NumberOfQuestions.ToString() },
            { PromptConstants.NumberOfAnswers, generationSettingsValue.NumberOfAnswers.ToString() },
            { PromptConstants.Topics, string.Join(',', generationSettingsValue.DefaultTopics) }
        };

        var formattedPrompt = templateRenderingService.Render(prompt, promptArguments);

        foundAssessment.Status = GenerationStatuses.Generating;
        await grammarAssessmentService.UpdateAsync(foundAssessment, cancellationToken: cancellationToken);

        var generatedQuestions = await policy.ExecuteAsync(async () =>
        {
            var result = await chatCompletionService.SendMessageAsync(formattedPrompt, cancellationToken: cancellationToken);
            return jsonSerializer.Deserialize<List<QuizQuestion>>(result.Content);
        });

        var skipSavingChanges = new CommandOptions(skipSavingChanges: true);

        // Validate question answers
        var validationErrors = generatedQuestions
            .SelectMany(question =>
            {
                var errors = new List<ValidationFailure>();

                // Check if the number of answers is correct
                if (question.Answers.Count != generationSettingsValue.NumberOfAnswers)
                {
                    errors.Add(new ValidationFailure(
                        nameof(question.Answers),
                        $"The question should have exactly {generationSettingsValue.NumberOfAnswers} answers, but it has {question.Answers.Count}."));
                }

                // Check for SingleChoice questions
                if (question.Type == QuestionType.SingleChoice && question.Answers.Count(a => a.IsCorrect) != 1)
                {
                    errors.Add(new ValidationFailure(
                        nameof(question.Answers),
                        "SingleChoice question must have exactly one correct answer."));
                }

                // Check for MultipleChoice questions
                if (question.Type == QuestionType.MultipleChoice && question.Answers.Count(a => a.IsCorrect) <= 1)
                {
                    errors.Add(new ValidationFailure(
                        nameof(question.Answers),
                        "MultipleChoice question must have more than one correct answer."));
                }

                return errors;
            })
            .ToList();

        if (validationErrors.Any())
            throw new AppValidationException(new ValidationException(validationErrors));
        
        // Save questions
        var answers = await Task.WhenAll(generatedQuestions
            .Take((int)generationSettingsValue.NumberOfQuestions)
            .Select(async (question, index) =>
            {
                var answers = question.Answers.OrderBy(a => a.IsCorrect).Take((int)generationSettingsValue.NumberOfAnswers).ToList();
                question.Answers = null!;
                question.AssessmentId = foundAssessment.Id;

                var createdQuestion = index != generatedQuestions.Count - 1
                    ? await quizQuestionService.CreateAsync(question, skipSavingChanges, cancellationToken)
                    : await quizQuestionService.CreateAsync(question, cancellationToken: cancellationToken);

                question.Answers = answers;
                answers.ForEach(a => a.QuizQuestion = createdQuestion);
                return answers;
            }));

        // Save answers
        var totalAnswers = answers.SelectMany(a => a).ToList();
        await Task.WhenAll(totalAnswers
            .Select(async (answer, index) =>
            {
                answer.QuestionId = answer.QuizQuestion.Id;

                return index != totalAnswers.Count - 1
                    ? await quizOptionService.CreateAsync(answer, skipSavingChanges, cancellationToken)
                    : await quizOptionService.CreateAsync(answer, cancellationToken: cancellationToken);
            }));

        foundAssessment.Status = GenerationStatuses.Completed;
        await grammarAssessmentService.UpdateAsync(foundAssessment, cancellationToken: cancellationToken);
    }
}