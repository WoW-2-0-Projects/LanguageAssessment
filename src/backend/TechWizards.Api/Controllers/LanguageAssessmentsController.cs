using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWizards.Application.GrammarAssessments.Commands;
using TechWizards.Application.GrammarAssessments.Models;
using TechWizards.Application.GrammarAssessments.Queries;
using TechWizards.Application.LanguageAssessments.Commands;
using TechWizards.Application.ListeningAssessments.Commands;
using TechWizards.Application.ListeningAssessments.Models;
using TechWizards.Application.ListeningAssessments.Queries;
using TechWizards.Application.QuizAssessments.Models;
using TechWizards.Application.QuizAssessmentSessions.Models;
using TechWizards.Application.QuizAssessmentSessions.Queries;
using TechWizards.Domain.Models.Enums;

namespace TechWizards.Api.Controllers;

/// <summary>
/// Controller for managing language assessments.
/// </summary>
[ApiController]
[Route("api/assessments/language")]
public class LanguageAssessmentsController(IMediatorBroker mediatorBroker) : ControllerBase
{
    #region General

    [HttpGet("{sessionId}/result")]
    [ProducesResponseType(typeof(QuizAssessmentSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetSessionResult(
        [FromRoute] Guid sessionId,
        CancellationToken cancellationToken)
    {
        var result = await mediatorBroker.SendAsync<QuizAssessmentSessionResultGetByIdQuery, QuizAssessmentSessionResultDto?>(
            new QuizAssessmentSessionResultGetByIdQuery(sessionId),
            new QueryOptions(),
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(QuizAssessmentSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateSession(CancellationToken cancellationToken)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? throw new InvalidOperationException("Cannot determine IP address");
        var result = await mediatorBroker.SendAsync<StartLanguageAssessmentSessionCommand, QuizAssessmentSessionDto>(
            new StartLanguageAssessmentSessionCommand(ipAddress),
            cancellationToken: cancellationToken);

        return Ok(result);
    }

    [HttpPut("{sessionId}/start")]
    [ProducesResponseType(typeof(QuizAssessmentSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> StartSession(
        [FromRoute] Guid sessionId,
        CancellationToken cancellationToken)
    {
        await mediatorBroker.SendAsync(
            new UpdateLanguageAssessmentSessionStateCommand(sessionId, AssessmentState.InProgress),
            new CommandOptions(),
            cancellationToken: cancellationToken);

        return NoContent();
    }

    [HttpPost("{sessionId}/cancel")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> CancelSession([FromRoute] Guid sessionId, CancellationToken cancellationToken)
    {
        var command = new EndLanguageAssessmentSessionCommand { SessionId = sessionId };
        await mediatorBroker.SendAsync(command, cancellationToken: cancellationToken);

        return Ok();
    }

    #endregion

    #region Grammar

    [HttpGet("grammar/{id}")]
    [ProducesResponseType(typeof(GrammarAssessmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetGrammarAssessmentById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await mediatorBroker.SendAsync<GrammarAssessmentGetByIdQuery, GrammarAssessmentDto?>(
            new GrammarAssessmentGetByIdQuery { Id = id },
            new QueryOptions(),
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("grammar/submit")]
    [ProducesResponseType(typeof(GrammarAssessmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> SubmitGrammarAssessment(
        [FromBody] SubmitGrammarAssessmentCommand command,
        CancellationToken cancellationToken)
    {
        await mediatorBroker.SendAsync(
            command,
            new CommandOptions(),
            cancellationToken);

        return Ok();
    }

    #endregion

    #region Listening

    [HttpGet("listening/{id}")]
    [ProducesResponseType(typeof(QuizAssessmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetListeningAssessmentById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await mediatorBroker.SendAsync<ListeningAssessmentGetByIdQuery, ListeningAssessmentDto?>(
            new ListeningAssessmentGetByIdQuery { Id = id },
            new QueryOptions(),
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("listening/submit")]
    [ProducesResponseType(typeof(GrammarAssessmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> SubmitListeningAssessment(
        [FromBody] SubmitListeningAssessmentCommand command,
        CancellationToken cancellationToken)
    {
        await mediatorBroker.SendAsync(
            command,
            new CommandOptions(),
            cancellationToken);

        return Ok();
    }

    #endregion
}