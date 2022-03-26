using FluentValidation;
using PSG.DeliveryService.Application.Validation.BaseValidators;
using PSG.DeliveryService.Application.ViewModels.OrderViewModels;

namespace PSG.DeliveryService.Application.Validation.OrderValidators;

public class CreateOrderViewModelValidator : AbstractValidator<CreateOrderViewModel>
{
    public CreateOrderViewModelValidator()
    {
        RuleFor(x => x.ClientId).SetValidator(new IdValidator<CreateOrderViewModel>("Client Id")!);
        RuleFor(x => x.To).MaximumLength(127).NotEqual(x => x.From);
        RuleFor(x => x.From).MaximumLength(127);
        RuleFor(x => x.OrderTime).SetValidator(new OrderTimeValidator<CreateOrderViewModel>()!);
        RuleFor(x => x.ProductName).MaximumLength(63);
    }
}