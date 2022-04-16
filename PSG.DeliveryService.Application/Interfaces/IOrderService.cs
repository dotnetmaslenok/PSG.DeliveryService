using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Queries.OrderQueries;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IOrderService
{
    public Task<Result<OrderResponse>> GetByIdAsync(GetOneOrderQuery getOneOrderQuery);

    public Task<Result<IEnumerable<OrderResponse>>> GetAll();

    public Task<Result<OrderResponse>> CreateAsync(CreateOrderCommand createOrderCommand);
}