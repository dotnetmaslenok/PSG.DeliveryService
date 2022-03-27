using Microsoft.AspNetCore.Identity;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task<Result<string ,string>> SignInAsync(SignInViewModel signInViewModel);

    public Task<Result<string, IEnumerable<IdentityError>>> SignUpAsync(SignUpViewModel signUpViewModel);

    public Task SignOutAsync();
}