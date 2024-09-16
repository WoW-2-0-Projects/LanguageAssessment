namespace TechWizards.Infrastructure.Common.Generation.Enums;

/// <summary>
/// Defines the types of events that occur during generation.
/// </summary>
public enum GenerationEventTypes
{
    /// <summary>
    /// Event triggered during validation.
    /// </summary>
    OnValidate,
    
    /// <summary>
    /// Event triggered during preparation.
    /// </summary>
    OnPrepare,
    
    /// <summary>
    /// Event triggered during generation.
    /// </summary>
    OnGenerate
}