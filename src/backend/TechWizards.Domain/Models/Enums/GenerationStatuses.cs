using Newtonsoft.Json;
using Temporary.Converters;

namespace TechWizards.Domain.Models.Enums;

/// <summary>
/// Defines the generation statuses.
/// </summary>
[JsonConverter(typeof(EnumToStringConverter<GenerationStatuses>))]
public enum GenerationStatuses : byte
{
    /// <summary>
    /// Refers to a state where the generation process has not started yet.
    /// </summary>
    NotStarted,

    /// <summary>
    /// Refers to a state where the generation process is currently ongoing.
    /// </summary>
    Generating,

    /// <summary>
    /// Refers to a state where the generation process has completed successfully.
    /// </summary>
    Completed,

    /// <summary>
    /// Refers to a state where the generation process has failed.
    /// </summary>
    Failed
}