using MediatR;
using ResultMonad;

namespace PSG.DeliveryService.Application.Commands;

public class SignInCommand : BaseAccountCommand, IRequest<Result<string, string>>
{
    
}