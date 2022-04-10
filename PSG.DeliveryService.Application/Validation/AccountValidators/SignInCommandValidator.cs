using FluentValidation;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Validation.BaseValidators;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<SignInCommand>()!);
    }
}