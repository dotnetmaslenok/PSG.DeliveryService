using MediatR;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries.OrderQueries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.OrderHandlers;

public sealed class GetOneOrderQueryHandler : IRequestHandler<GetOneOrderQuery, Result<OrderResponse>>
{
    private readonly IOrderService _orderService;
    
    public GetOneOrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<Result<OrderResponse>> Handle(GetOneOrderQuery getOneOrderQuery, CancellationToken cancellationToken)
    {
        return await _orderService.GetByIdAsync(getOneOrderQuery);
    }
}