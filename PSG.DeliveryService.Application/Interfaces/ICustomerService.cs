using PSG.DeliveryService.Application.Queries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface ICustomerService
{
    public Task<Result<UserResponse>> GetByIdAsync(UserQuery userQuery);
}