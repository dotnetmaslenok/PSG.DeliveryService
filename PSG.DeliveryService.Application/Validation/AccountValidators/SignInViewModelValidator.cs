using FluentValidation;
using PSG.DeliveryService.Application.Validation.BaseValidators;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
{
    public SignInViewModelValidator()
    {
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<SignInViewModel>()!);
    }
}