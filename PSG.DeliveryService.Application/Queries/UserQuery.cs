using MediatR;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries;

public sealed class UserQuery : IRequest<Result<UserResponse>>
{
    public Guid Id { get; set; }
    public UserQuery(string id)
    {
        Id = GuidConverter.Decode(id);
    }
}