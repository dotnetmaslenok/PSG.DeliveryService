using FluentValidation;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
{
    public SignUpViewModelValidator()
    {
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(255);
    }
}