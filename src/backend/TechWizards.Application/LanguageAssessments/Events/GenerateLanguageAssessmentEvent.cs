using Backbone.Comms.Infra.Abstractions.Events;
using TechWizards.Domain.Models.Entities;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Application.LanguageAssessments.Events;

/// <summary>
/// Represents an event for generating language assessment.
/// </summary>
public sealed record GenerateLanguageAssessmentEvent(ICollection<(Guid Id, string Type)> Assessments) : EventBase;