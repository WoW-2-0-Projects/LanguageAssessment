using Newtonsoft.Json;
using Temporary.Converters;

namespace TechWizards.Domain.Models.Enums;

/// <summary>
/// Defines assessment states
/// </summary>
[JsonConverter(typeof(EnumToStringConverter<AssessmentState>))]
public enum AssessmentState : byte
{
    /// <summary>
    /// Refers to an assessment that has not been started yet.
    /// </summary>
    NotStarted,

    /// <summary>
    /// Refers to an assessment currently in progress.
    /// </summary>
    InProgress,

    /// <summary>
    /// Refers to an assessment that has been completed successfully.
    /// </summary>
    Completed,

    /// <summary>
    /// Refers to an assessment that has been canceled before completion.
    /// </summary>
    Canceled,

    /// <summary>
    /// Refers to an assessment that has been forcibly terminated, possibly due to an error or external intervention.
    /// </summary>
    Terminated
}