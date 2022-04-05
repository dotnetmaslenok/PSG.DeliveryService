using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize("Bearer")]
    public async Task<IActionResult> GetUserByIdAsync([FromQuery(Name = "u")] string userId)
    {
        var userQuery = new UserQuery(userId);
        var result = await _mediator.Send(userQuery);

        if (result.IsFailure)
        {
            return NotFound();
        }
        
        return Ok(result.Value);
    }
}