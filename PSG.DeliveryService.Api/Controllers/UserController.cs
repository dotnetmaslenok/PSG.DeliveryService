using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Queries;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByIdAsync([FromQuery(Name = "u")] Guid userId)
    {
        var userQuery = new UserQuery(userId);
        var result = await _mediator.Send(userQuery);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound();
    }
}