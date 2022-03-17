using PSG.DeliveryService.Application.ViewModels.OrderViewModels;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IOrderService
{
    public Task<int> CreateOrderAsync(CreateOrderViewModel createOrderViewModel);
}