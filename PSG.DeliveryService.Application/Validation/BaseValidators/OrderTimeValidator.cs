using FluentValidation;
using FluentValidation.Validators;

namespace PSG.DeliveryService.Application.Validation.BaseValidators;

public class OrderTimeValidator<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (DateTime.TryParse(value, out DateTime dateTime))
        {
            return dateTime >= DateTime.Now.AddMinutes(30);
        }

        return false;
    }

    public override string Name { get; } = nameof(OrderTimeValidator<T>);
}