using FluentValidation;
using PSG.DeliveryService.Application.Validation.BaseValidators;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
{
    public SignUpViewModelValidator()
    {
        RuleFor(x => x.UserName).MinimumLength(8).MaximumLength(31);
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<SignUpViewModel>()!);
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password);
    }
}