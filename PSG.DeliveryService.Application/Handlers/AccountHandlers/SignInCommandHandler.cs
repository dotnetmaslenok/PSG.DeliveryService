using MediatR;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.AccountHandlers;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<string, string>>
{
    private readonly IAccountService _accountService;
    
    public SignInCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task<Result<string, string>> Handle(SignInCommand signInCommand, CancellationToken cancellationToken)
    {
        return await _accountService.SignInAsync(signInCommand);
    }
}