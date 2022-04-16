using MediatR;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries.UserQueries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Handlers.UserHandlers;

public sealed class GetOneUserQueryHandler : IRequestHandler<GetOneUserQuery, Result<UserResponse>>
{
    private readonly ICustomerService _customerService;
    
    public GetOneUserQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    public async Task<Result<UserResponse>> Handle(GetOneUserQuery getOneUserQuery, CancellationToken cancellationToken)
    {
        return await _customerService.GetByIdAsync(getOneUserQuery);
    }
}