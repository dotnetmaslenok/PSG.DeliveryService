using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUpAsync([FromForm]RegistrationCommand registrationCommand)
    { 
        var result = await _mediator.Send(registrationCommand);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        
        return BadRequest(result.Error);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync([FromForm]SignInCommand signInCommand)
    {
        var result = await _mediator.Send(signInCommand);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
    
    [HttpPost("sign-out")]
    [Authorize]
    public async Task<IActionResult> SignOutAsync()
    {
        await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

        return NoContent();
    }
}