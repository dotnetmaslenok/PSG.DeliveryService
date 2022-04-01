using MediatR;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries;

public record UserQuery(Guid Id) : IRequest<Result<UserResponse>>;