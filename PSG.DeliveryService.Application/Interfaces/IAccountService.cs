using PSG.DeliveryService.Application.ExceptionHandling;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task<TResult> SignInAsync(SignInViewModel signInViewModel);

    public Task<TResult> SignUpAsync(SignUpViewModel signUpViewModel);

    public Task<TResult> SignOutAsync();
}