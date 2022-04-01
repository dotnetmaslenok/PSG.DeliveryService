using MediatR;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.UserHandlers;

public class UserQueryHandler : IRequestHandler<UserQuery, Result<UserResponse>>
{
    private readonly IUserService _userService;
    
    public UserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Result<UserResponse>> Handle(UserQuery userQuery, CancellationToken cancellationToken)
    {
        return await _userService.GetUserByIdAsync(userQuery);
    }
}