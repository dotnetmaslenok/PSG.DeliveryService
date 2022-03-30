using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IOrderService
{
    public Task<Result<OrderResponse>> GetOrderByIdAsync(Guid orderId);
    
    public Task<Result<CreateOrderCommand, Exception>> CreateOrderAsync(CreateOrderCommand createOrderCommand);
}