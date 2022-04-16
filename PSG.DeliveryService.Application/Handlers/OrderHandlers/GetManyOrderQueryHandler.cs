using MediatR;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries.OrderQueries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.OrderHandlers;

public sealed class GetManyOrderQueryHandler : IRequestHandler<GetManyOrderQuery, Result<IEnumerable<OrderResponse>>>
{
    private readonly IOrderService _orderService;
    
    public GetManyOrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<Result<IEnumerable<OrderResponse>>> Handle(GetManyOrderQuery request, CancellationToken cancellationToken)
    {
        return await _orderService.GetAll();
    }
}