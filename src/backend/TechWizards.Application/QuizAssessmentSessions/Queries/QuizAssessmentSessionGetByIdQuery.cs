using Backbone.Comms.Infra.Abstractions.Queries;
using TechWizards.Application.QuizAssessmentSessions.Models;

namespace TechWizards.Application.QuizAssessmentSessions.Queries;

/// <summary>
/// Represents a query for retrieving an assessment session result by its ID.
/// </summary>
public record QuizAssessmentSessionGetByIdQuery(Guid Id) : IQuery<QuizAssessmentSessionDto?>;