using MediatR;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.UserHandlers;

public sealed class UserQueryHandler : IRequestHandler<UserQuery, Result<UserResponse>>
{
    private readonly ICustomerService _customerService;
    
    public UserQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    public async Task<Result<UserResponse>> Handle(UserQuery userQuery, CancellationToken cancellationToken)
    {
        return await _customerService.GetByIdAsync(userQuery);
    }
}