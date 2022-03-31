using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.AccountHandlers;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<JsonResult, string>>
{
    private readonly IAccountService _accountService;
    
    public SignInCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task<Result<JsonResult, string>> Handle(SignInCommand signInCommand, CancellationToken cancellationToken)
    {
        return await _accountService.SignInAsync(signInCommand);
    }
}