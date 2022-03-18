using FluentValidation;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
{
    public SignInViewModelValidator()
    {
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}