using Backbone.Comms.Infra.Abstractions.Commands;
using TechWizards.Application.QuizAssessmentSessions.Models;

namespace TechWizards.Application.LanguageAssessments.Commands;

/// <summary>
/// Represents a command for starting assessment session
/// </summary>
public sealed record StartLanguageAssessmentSessionCommand(string IpAddress) : ICommand<QuizAssessmentSessionDto>;