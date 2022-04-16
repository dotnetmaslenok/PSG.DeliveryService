using MediatR;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries.UserQueries;

public sealed class GetOneUserQuery : IRequest<Result<UserResponse>>
{
    public Guid Id { get; set; }
    public GetOneUserQuery(string id)
    {
        Id = GuidConverter.Decode(id);
    }
}