using FluentValidation;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Validation.BaseValidators;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<RegistrationCommand>()!);
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password);
    }
}