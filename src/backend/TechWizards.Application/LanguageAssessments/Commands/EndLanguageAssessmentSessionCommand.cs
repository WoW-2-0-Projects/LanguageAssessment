using Backbone.Comms.Infra.Abstractions.Commands;

namespace TechWizards.Application.LanguageAssessments.Commands;

/// <summary>
/// Represents a command for ending assessment session
/// </summary>
public sealed record EndLanguageAssessmentSessionCommand: ICommand
{
    /// <summary>
    /// Gets the language session identifier.
    /// </summary>
    public Guid SessionId { get; set; }
}