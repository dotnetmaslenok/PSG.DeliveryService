using System.Text.RegularExpressions;
using FluentValidation;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Validation.BaseValidators;

namespace PSG.DeliveryService.Application.Validation.AccountValidators;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.PhoneNumber).Matches(Regex.Escape(@"^[+][7]-[(]\d{3}[)][-]\d{3}[-]\d{2}-\d{2}"));
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<SignInCommand>()!);
    }
}