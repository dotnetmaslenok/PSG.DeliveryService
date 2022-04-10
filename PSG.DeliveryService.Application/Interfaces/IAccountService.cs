using Microsoft.AspNetCore.Identity;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task<Result<AuthenticationResponse, IEnumerable<IdentityError>>> RegisterAsync(RegistrationCommand registrationRequest);
    
    public Task<Result<AuthenticationResponse, string>> SignInAsync(SignInCommand signInCommand);
}