using MediatR;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries;

public class OrderQuery : IRequest<Result<OrderResponse>>
{
    public Guid Id { get; }

    public OrderQuery(Guid id)
    {
        Id = id;
    }
}