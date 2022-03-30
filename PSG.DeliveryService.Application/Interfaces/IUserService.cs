using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IUserService
{
    public Task<Result<UserResponse>> GetUserByIdAsync(Guid userId);
}