using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.ViewModels.OrderViewModels;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Application.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<int> CreateOrderAsync(CreateOrderViewModel createOrderViewModel)
    {
        int.TryParse(createOrderViewModel.ClientId, out int clientId);

        var customer = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == clientId);

        //TODO: Добавить логику распределения заказов между курьерами
        var courier = await _dbContext.Users.LastAsync();

        var order = _mapper.Map<CreateOrderViewModel, Order>(createOrderViewModel);
        order.Customer = customer!;
        order.Courier = courier;

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order.Id;
    }
}