using FluentValidation;
using FluentValidation.Validators;

namespace PSG.DeliveryService.Application.Validation.BaseValidators;

public class IdValidator<T> : PropertyValidator<T, string>
{
    private readonly string _fieldName;

    public IdValidator(string fieldName)
    {
        _fieldName = fieldName;
    }


    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (int.TryParse(value, out int id))
        {
            return id > 0;
        }

        context.AddFailure(_fieldName, $"{_fieldName} must be greater then 0");
        return false;
    }

    public override string Name { get; } = nameof(IdValidator<T>);
}