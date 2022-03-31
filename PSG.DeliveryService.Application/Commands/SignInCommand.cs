using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResultMonad;

namespace PSG.DeliveryService.Application.Commands;

public class SignInCommand : BaseAccountCommand, IRequest<Result<JsonResult, string>>
{
    
}