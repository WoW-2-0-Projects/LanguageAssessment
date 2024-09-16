using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Events;
using TechWizards.Application.GrammarAssessments.Events;
using TechWizards.Application.LanguageAssessments.Events;
using TechWizards.Application.ListeningAssessments.Events;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;
using TechWizards.Infrastructure.Common.Constants;
using TechWizards.Infrastructure.LanguageAssessments.Constants;

namespace TechWizards.Infrastructure.LanguageAssessments.EventHandlers;

/// <summary>
/// Handles the execution of the <see cref="GenerateLanguageAssessmentEvent"/>.
/// </summary>
public class GenerateLanguageAssessmentEventHandler(IEventBusBroker eventBusBroker) : EventHandlerBase<GenerateLanguageAssessmentEvent>
{
    protected override async ValueTask HandleAsync(GenerateLanguageAssessmentEvent eventContext, CancellationToken cancellationToken)
    {
        await Task.WhenAll(eventContext.Assessments.Select(async assessment =>
        {
            if (!Enum.TryParse<LanguageAssessmentTypes>(assessment.Type, out var type))
                throw new InvalidOperationException($"Invalid assessment type - {assessment.Type}.");

            var publishEventTask = type switch
            {
                LanguageAssessmentTypes.Grammar => eventBusBroker.PublishAsync(
                    new GenerateGrammarAssessmentEvent(assessment.Id),
                    new EventOptions
                    {
                        Exchange = EventBusConstants.LanguageAssessmentExchangeName,
                        RoutingKey = EventBusConstants.GrammarAssessmentGenerationQueueName,
                        IsPersistent = true
                    },
                    cancellationToken),
                LanguageAssessmentTypes.Listening => eventBusBroker.PublishAsync(
                    new GenerateListeningAssessmentEvent(assessment.Id),
                    new EventOptions
                    {
                        Exchange = EventBusConstants.LanguageAssessmentExchangeName,
                        RoutingKey = EventBusConstants.ListeningAssessmentGenerationQueueName,
                        IsPersistent = true
                    },
                    cancellationToken),
                _ => throw new NotSupportedException($"This type of assessment isn't supported to generate - {type}.")
            };

            await publishEventTask;
        }));
    }
}