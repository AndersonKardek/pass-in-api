using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAllByEventId;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    private readonly GetAllAttendeeByEventIdUseCase _getAllAttendeeByEventIdUseCase;
    private readonly RegisterAttendeeOnEventUseCase _registerAttendeeOnEventUseCase;

    public AttendeesController(GetAllAttendeeByEventIdUseCase getAllAttendeeByEventIdUseCase, RegisterAttendeeOnEventUseCase registerAttendeeOnEventUseCase)
    {
        _getAllAttendeeByEventIdUseCase = getAllAttendeeByEventIdUseCase;
        _registerAttendeeOnEventUseCase = registerAttendeeOnEventUseCase;
    }

    [HttpPost]
    [Route("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        var response = await _registerAttendeeOnEventUseCase.Execute(eventId, request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesjson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromRoute] Guid eventId)
    {
        var response = await _getAllAttendeeByEventIdUseCase.Execute(eventId);
        return Ok(response);
    }
}
