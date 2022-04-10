using FluentValidation;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Validation.BaseValidators;

namespace PSG.DeliveryService.Application.Validation.OrderValidators;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.DeliveryAddress).MaximumLength(127).NotEqual(x => x.ProductAddress);
        RuleFor(x => x.ProductAddress).MaximumLength(127);
        RuleFor(x => x.OrderTime).SetValidator(new OrderTimeValidator<CreateOrderCommand>());
        RuleFor(x => x.ProductName).MaximumLength(63);
    }
}