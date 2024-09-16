using Backbone.Comms.Infra.Abstractions.Events;
using TechWizards.Domain.Models.Entities;

namespace TechWizards.Application.GrammarAssessments.Events;

/// <summary>
/// Represents an event for generating grammar assessment.
/// </summary>
public sealed record GenerateGrammarAssessmentEvent(Guid GrammarAssessmentId) : EventBase;