using MediatR;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.OrderHandlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderResponse>>
{
    private readonly IOrderService _orderService;
    
    public CreateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<Result<OrderResponse>> Handle(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        return await _orderService.CreateOrderAsync(createOrderCommand);
    }
}