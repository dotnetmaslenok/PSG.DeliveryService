using MediatR;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries.OrderQueries;

public sealed class GetManyOrderQuery : IRequest<Result<IEnumerable<OrderResponse>>>
{
    
}