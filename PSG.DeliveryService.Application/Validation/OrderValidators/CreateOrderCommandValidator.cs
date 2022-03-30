using FluentValidation;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Validation.BaseValidators;

namespace PSG.DeliveryService.Application.Validation.OrderValidators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.To).MaximumLength(127).NotEqual(x => x.From);
        RuleFor(x => x.From).MaximumLength(127);
        RuleFor(x => x.OrderTime).SetValidator(new OrderTimeValidator<CreateOrderCommand>()!);
        RuleFor(x => x.ProductName).MaximumLength(63);
    }
}