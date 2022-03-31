using MediatR;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.OrderHandlers;

public class OrderQueryHandler : IRequestHandler<OrderQuery, Result<OrderResponse>>
{
    private readonly IOrderService _orderService;
    
    public OrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<Result<OrderResponse>> Handle(OrderQuery orderQuery, CancellationToken cancellationToken)
    {
        return await _orderService.GetOrderByIdAsync(orderQuery);
    }
}