using FluentValidation;
using PSG.DeliveryService.Application.Validation.BaseValidators;
using PSG.DeliveryService.Application.ViewModels.OrderViewModels;

namespace PSG.DeliveryService.Application.Validation.OrderValidators;

public class CreateOrderViewModelValidator : AbstractValidator<CreateOrderViewModel>
{
    public CreateOrderViewModelValidator()
    {
        RuleFor(x => x.ClientId).SetValidator(new IdValidator<CreateOrderViewModel>("Client Id"));
        RuleFor(x => x.To).MaximumLength(255).NotEmpty().NotEqual(x => x.From);
        RuleFor(x => x.From).MaximumLength(255).NotEmpty();
        RuleFor(x => x.ProductName).MaximumLength(255).NotEmpty();
    }
}