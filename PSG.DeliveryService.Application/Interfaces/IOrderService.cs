using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Queries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IOrderService
{
    public Task<Result<OrderResponse>> GetByIdAsync(OrderQuery orderQuery);
    
    public Task<Result<OrderResponse>> CreateAsync(CreateOrderCommand createOrderCommand);
}