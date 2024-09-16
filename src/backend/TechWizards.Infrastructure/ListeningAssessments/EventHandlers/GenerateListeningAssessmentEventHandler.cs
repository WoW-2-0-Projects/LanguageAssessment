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
using TechWizards.Application.ListeningAssessments.Events;
using TechWizards.Application.ListeningAssessments.Services;
using TechWizards.Application.QuizOptions.Services;
using TechWizards.Application.QuizQuestions.Services;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;
using TechWizards.Infrastructure.LanguageAssessments.Constants;
using TechWizards.Infrastructure.ListeningAssessments.Settings;
using Temporary;
using Temporary.Extensions;
using Temporary.TextTemplates.Services;

namespace TechWizards.Infrastructure.ListeningAssessments.EventHandlers;

/// <summary>
/// Handles the execution of the <see cref="GenerateListeningAssessmentEvent"/>.
/// </summary>
public class GenerateListeningAssessmentEventHandler(
    ILogger<GenerateListeningAssessmentEventHandler> logger,
    IOptions<ListeningAssessmentGenerationSettings> generationSettings,
    IOptions<DomainSettings> domainSettings,
    IOptions<LocalFileStorageSettings> localFileStorageSettings,
    IServiceScopeFactory serviceScopeFactory,
    IJsonSerializer jsonSerializer,
    IChatCompletionService chatCompletionService,
    ITextTemplateRenderingService templateRenderingService,
    ITextToSpeechService textToSpeechService
) : EventHandlerBase<GenerateListeningAssessmentEvent>
{
    protected override async ValueTask HandleAsync(GenerateListeningAssessmentEvent eventContext, CancellationToken cancellationToken)
    {
        var generationSettingsValue = generationSettings.Value;
        var localFileStorageSettingsValue = localFileStorageSettings.Value;
        var domainSettingsValue = domainSettings.Value;
        var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

        var listeningAssessmentService = serviceProvider.GetRequiredService<IListeningAssessmentService>();
        var quizQuestionService = serviceProvider.GetRequiredService<IQuizQuestionService>();
        var quizOptionService = serviceProvider.GetRequiredService<IQuizOptionService>();

        var foundAssessment = await listeningAssessmentService.GetByIdAsync(eventContext.ListeningAssessmentId, cancellationToken: cancellationToken)
                              ?? throw new InvalidOperationException(
                                  $"Listening assessment with id {eventContext.ListeningAssessmentId} was not found.");

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

        const string audioContentGenerationPrompt =
            """
                ## Concepts
            
                Listening assessment - english language assessment focusing on listening skills.
                Listening text - text for listening that will be converted to audio
            
                ## Instructions
            
                Generate a text for listening assessment with the following requirements:
            
                ## Requirements :
            
                - minimum number of words - {{MinNumberOfWords}}
                - maximum number of words - {{MaxNumberOfWords}}
                - topics - {{Topics}}
                - result format must in plain text without any serialization or formatting

            """;

        var promptArguments = new Dictionary<string, string>
        {
            { PromptConstants.MinNumberOfWords, generationSettingsValue.MinNumberOfWords.ToString() },
            { PromptConstants.MaxNumberOfWords, generationSettingsValue.MaxNumberOfWords.ToString() },
            { PromptConstants.Topics, string.Join(',', generationSettingsValue.DefaultTopics) }
        };

        foundAssessment.Status = GenerationStatuses.Generating;
        await listeningAssessmentService.UpdateAsync(foundAssessment, cancellationToken: cancellationToken);
        var formattedPrompt = templateRenderingService.Render(audioContentGenerationPrompt, promptArguments);

        var result = await policy.ExecuteAsync(async () =>
            await chatCompletionService.SendMessageAsync(formattedPrompt, cancellationToken: cancellationToken));

        if (string.IsNullOrWhiteSpace(result.Content))
            throw new InvalidOperationException("Listening text was not generated.");

        // Convert listening content text to audio format
        foundAssessment.AudioContent = result.Content;

        // Convert listening content to an audio file
        var fileContent = await textToSpeechService.TextToSpeechAsync(foundAssessment.AudioContent, "alloy", cancellationToken: cancellationToken);

        var fileName = $"{Guid.NewGuid()}.mp3";
        var fileRelativePath = Path.Combine("audio", fileName);

        // Combine the wwwroot path with a folder name (e.g., "audio") and the file name
        var filePath = Path.Combine(localFileStorageSettingsValue.RootPath, fileRelativePath);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        await File.WriteAllBytesAsync(filePath, fileContent, cancellationToken);

        var fileAbsoluteUrl = $"{domainSettingsValue.ApiDomainUrl}/{fileRelativePath.ToUrl()}";

        foundAssessment.AudioFileUrl = fileAbsoluteUrl;

        // Update listening audio content
        await listeningAssessmentService.UpdateAsync(foundAssessment, cancellationToken: cancellationToken);

        const string contentQuestionsGenerationPrompt =
            """
                ## Concepts
            
                Listening assessment - english language assessment focusing on listening skills.
                Listening text - text for listening that will be converted to audio
            
                ## Instructions
            
                Generate questions based on given text for listening assessment with the following requirements:
            
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
            
            Listening assessment text - {{ListeningAudioContent}}

            """;

        promptArguments = new Dictionary<string, string>
        {
            { PromptConstants.QuestionTypes, string.Join(',', EnumExtensions.GetAllDescriptions<QuestionType>()) },
            { PromptConstants.AssessmentLevel, foundAssessment.Level.ToString() },
            { PromptConstants.NumberOfQuestions, generationSettingsValue.NumberOfQuestions.ToString() },
            { PromptConstants.NumberOfAnswers, generationSettingsValue.NumberOfAnswers.ToString() },
            { PromptConstants.Topics, string.Join(',', generationSettingsValue.DefaultTopics) },
            { PromptConstants.ListeningAudioContent, foundAssessment.AudioContent },
        };

        formattedPrompt = templateRenderingService.Render(contentQuestionsGenerationPrompt, promptArguments);

        var generatedQuestions = await policy.ExecuteAsync(async () =>
        {
            result = await chatCompletionService.SendMessageAsync(formattedPrompt, cancellationToken: cancellationToken);
            return jsonSerializer.Deserialize<List<QuizQuestion>>(result.Content);
        });

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

        var skipSavingChanges = new CommandOptions(skipSavingChanges: true);

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
        await listeningAssessmentService.UpdateAsync(foundAssessment, cancellationToken: cancellationToken);
    }
}