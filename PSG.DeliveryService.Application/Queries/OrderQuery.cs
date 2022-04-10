using MediatR;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries;

public sealed class OrderQuery : IRequest<Result<OrderResponse>>
{
    public Guid Id { get; set; }
    public OrderQuery(string id)
    {
        Id = GuidConverter.Decode(id);
    }
}