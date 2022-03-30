using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Responses;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;
using ResultMonad;

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
    
    public async Task<Result<OrderResponse>> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        if (order is null)
        {
            return Result.Fail<OrderResponse>();
        }

        var orderResponse = _mapper.Map<OrderResponse>(order);
        return Result.Ok(orderResponse);
    }
    
    public async Task<Result<CreateOrderCommand, Exception>> CreateOrderAsync(CreateOrderCommand createOrderCommand)
    {
        var order = _mapper.Map<Order>(createOrderCommand);

        try
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Fail<CreateOrderCommand, Exception>(e);
        }

        return Result.Ok<CreateOrderCommand, Exception>(createOrderCommand);
    }
}