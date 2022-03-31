using MediatR;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.OrderHandlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderCommand>>
{
    private readonly IOrderService _orderService;
    
    public CreateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<Result<CreateOrderCommand>> Handle(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        return await _orderService.CreateOrderAsync(createOrderCommand);
    }
}