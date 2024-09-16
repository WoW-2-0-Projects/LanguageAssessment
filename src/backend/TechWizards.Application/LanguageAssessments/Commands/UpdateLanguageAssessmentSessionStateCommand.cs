using Backbone.Comms.Infra.Abstractions.Commands;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Application.LanguageAssessments.Commands;

/// <summary>
/// Represents a command to update the status of language assessment session
/// </summary>
public sealed record UpdateLanguageAssessmentSessionStateCommand(Guid SessionId, AssessmentState NewState) : ICommand;