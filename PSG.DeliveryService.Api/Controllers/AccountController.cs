using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync([FromForm]SignUpViewModel signUpViewModel)
    { 
        var result = await _accountService.SignUpAsync(signUpViewModel);

        if (result.IsSuccess)
        {
            return Ok(Results.Json(result.Value));
        }
        
        return BadRequest(Results.Json(result.Error));
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync([FromForm]SignInViewModel signInViewModel)
    {
        var result = await _accountService.SignInAsync(signInViewModel);
        
        if (result.IsSuccess)
        {
            return Ok(Results.Json(result.Value));
        }

        return BadRequest(Results.Json(result.Error));
    }

    [Authorize]
    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOutAsync()
    {
        await _accountService.SignOutAsync();

        return NoContent();
    }
}