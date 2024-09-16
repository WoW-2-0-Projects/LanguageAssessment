using Backbone.Comms.Infra.Abstractions.Events;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Application.ListeningAssessments.Events;

/// <summary>
/// Represents an event for generating listening assessment.
/// </summary>
public sealed record GenerateListeningAssessmentEvent(Guid ListeningAssessmentId) : EventBase;