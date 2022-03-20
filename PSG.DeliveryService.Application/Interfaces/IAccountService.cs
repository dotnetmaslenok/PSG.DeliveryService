using Microsoft.AspNetCore.Identity;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task<ResultWithError<string>> SignInAsync(SignInViewModel signInViewModel);

    public Task<ResultWithError<IEnumerable<IdentityError>>> SignUpAsync(SignUpViewModel signUpViewModel);

    public Task<Result> SignOutAsync();
}