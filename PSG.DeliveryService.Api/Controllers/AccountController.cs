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
    public async Task<IActionResult> SignUpAsync(SignUpViewModel signUpViewModel)
    {
        var result = await _accountService.SignUpAsync(signUpViewModel);

        if (result.Succeded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(SignInViewModel signInViewModel)
    {
        var result = await _accountService.SignInAsync(signInViewModel);

        if (result.Succeded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> SignOutAsync()
    {
        var result = await _accountService.SignOutAsync();

        if (result.Succeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}