using Microsoft.AspNetCore.Identity;
using PSG.DeliveryService.Application.Commands;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task<Result<string, IEnumerable<IdentityError>>> CreateAsync(RegistrationCommand registrationRequest);
    
    public Task<Result<string ,string>> SignInAsync(SignInCommand signInCommand);

    public Task SignOutAsync();
}