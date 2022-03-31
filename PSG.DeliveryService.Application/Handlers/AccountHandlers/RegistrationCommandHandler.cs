using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.AccountHandlers;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Result<JsonResult, IEnumerable<IdentityError>>>
{
    private readonly IAccountService _accountService;
    
    public RegistrationCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task<Result<JsonResult, IEnumerable<IdentityError>>> Handle(RegistrationCommand registrationCommand, CancellationToken cancellationToken)
    {
        return await _accountService.CreateAsync(registrationCommand);
    }
}