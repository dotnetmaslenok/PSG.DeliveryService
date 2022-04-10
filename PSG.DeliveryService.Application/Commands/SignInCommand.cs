using MediatR;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Commands;

public sealed class SignInCommand : BaseAccountCommand, IRequest<Result<AuthenticationResponse, string>>
{
    
}