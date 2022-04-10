using MediatR;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.AccountHandlers;

public sealed class SignInCommandHandler : IRequestHandler<SignInCommand, Result<AuthenticationResponse, string>>
{
    private readonly IAccountService _accountService;
    
    public SignInCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task<Result<AuthenticationResponse, string>> Handle(SignInCommand signInCommand, CancellationToken cancellationToken)
    {
        return await _accountService.SignInAsync(signInCommand);
    }
}