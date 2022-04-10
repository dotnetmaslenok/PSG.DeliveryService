using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace PSG.DeliveryService.Application.Validation.BaseValidators;

public sealed class PasswordValidator<T> : PropertyValidator<T,string>
{
    private const int MinLength = 8;
    private const int MaxLength = 31;
    private const string PropertyName = "Password";
    
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var isValid = true;

        if (value.Length < MinLength)
        {
            context.AddFailure(PropertyName, $"{PropertyName} length must be greater or equal {MinLength}");
            isValid = false;
        }

        if (value.Length > MaxLength)
        {
            context.AddFailure(PropertyName, $"{PropertyName} length must be less or equal {MaxLength}");
            isValid = false;
        }

        if (!Regex.IsMatch(value, "[0-9]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} must contains at least 1 digit");
            isValid = false;
        }

        if (!Regex.IsMatch(value, "[a-z]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} must contains at least 1 lowercase letter");
            isValid = false;
        }

        if (!Regex.IsMatch(value, "[A-Z]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} must contains at least 1 uppercase letter");
            isValid = false;
        }

        return isValid;
    }
    
    public override string Name { get; } = nameof(PasswordValidator<T>);
}