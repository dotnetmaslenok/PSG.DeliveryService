using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.ViewModels.OrderViewModels;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Domain.Enums;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Application.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;

    public OrderService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> CreateOrderAsync(CreateOrderViewModel createOrderViewModel)
    {
        const int fastDeliveryTime = 30;
        const decimal deliveryPrice = 500m;
	    
        int.TryParse(createOrderViewModel.OrderWeight, out int orderWeight);
        int.TryParse(createOrderViewModel.OrderType, out int orderType);
        int.TryParse(createOrderViewModel.DeliveryType, out int deliveryType);

        var customer = await _dbContext.Users.FirstAsync();
        var courier = await _dbContext.Users.LastAsync();

        var random = new Random();
	    
        var order = new Order
        {
            OrderWeight = (OrderWeight)orderWeight,
            OrderType = (OrderType)orderType,
            DeliveryType = (DeliveryType)deliveryType,
            ProductName = createOrderViewModel.ProductName,
            ProductAddress = createOrderViewModel.From,
            DeliveryAddress = createOrderViewModel.To,
            OrderTime = !string.IsNullOrEmpty(createOrderViewModel.OrderTime) ? Convert.ToDateTime(createOrderViewModel.OrderTime) : DateTime.UtcNow.AddMinutes(fastDeliveryTime),
            DeliveryPrice = deliveryPrice,
            Customer = customer,
            Courier = courier,
            ProductPrice = deliveryPrice * (decimal)(random.NextDouble() + 1)
        };

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order.Id;
    }
}