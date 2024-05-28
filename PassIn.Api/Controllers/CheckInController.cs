using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Checkins.DoCheckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    private readonly DoAttendeeCheckInUseCase _doAttendeeCheckInUseCase;

    public CheckInController(DoAttendeeCheckInUseCase doAttendeeCheckInUseCase)
    {
        _doAttendeeCheckInUseCase = doAttendeeCheckInUseCase;     
    }

    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckIn([FromRoute] Guid attendeeId)
    {
        var response = await _doAttendeeCheckInUseCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}
