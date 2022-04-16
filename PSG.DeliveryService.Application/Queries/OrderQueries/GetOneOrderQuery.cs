using MediatR;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries.OrderQueries;

public sealed class GetOneOrderQuery : IRequest<Result<OrderResponse>>
{
    public Guid Id { get; set; }
    public GetOneOrderQuery(string id)
    {
        Id = GuidConverter.Decode(id);
    }
}