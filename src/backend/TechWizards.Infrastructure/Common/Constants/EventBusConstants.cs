namespace TechWizards.Infrastructure.Common.Constants;

/// <summary>
/// Contains constants related to the event bus configuration
/// </summary>
public static class EventBusConstants
{
    #region Language Assessment
    
    public const string LanguageAssessmentExchangeName = "LanguageAssessment";
    public const string LanguageAssessmentGenerationQueueName = "LanguageAssessmentGeneration";
    public const string GrammarAssessmentGenerationQueueName = "GrammarAssessmentGeneration";
    public const string ListeningAssessmentGenerationQueueName = "ListeningAssessmentGeneration";
    
    #endregion
}