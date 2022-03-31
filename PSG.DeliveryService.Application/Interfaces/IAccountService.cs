using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task<Result<JsonResult, IEnumerable<IdentityError>>> CreateAsync(RegistrationCommand registrationRequest);
    
    public Task<Result<JsonResult ,string>> SignInAsync(SignInCommand signInCommand);

    public Task SignOutAsync();
}