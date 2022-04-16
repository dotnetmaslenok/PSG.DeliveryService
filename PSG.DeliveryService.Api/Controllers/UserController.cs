using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Queries.UserQueries;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    [Authorize("Bearer")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string userId)
    {
        var query = new GetOneUserQuery(userId);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound();
        }
        
        return Ok(result.Value);
    }
}