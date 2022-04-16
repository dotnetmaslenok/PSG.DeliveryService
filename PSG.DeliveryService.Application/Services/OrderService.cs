using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries.OrderQueries;
using PSG.DeliveryService.Application.Responses;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;
using ResultMonad;

namespace PSG.DeliveryService.Application.Services;

public sealed class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Result<OrderResponse>> GetByIdAsync(GetOneOrderQuery getOneOrderQuery)
    {
        var order = await _dbContext.Orders
            .Include(x => x.Customer)
            .Include(x => x.Courier)
            .FirstOrDefaultAsync(x => x.Id == getOneOrderQuery.Id);
        
        if (order is null)
        {
            return Result.Fail<OrderResponse>();
        }

        return Result.Ok(_mapper.Map<OrderResponse>(order));
    }

    public async Task<Result<IEnumerable<OrderResponse>>> GetAll()
    {
        var orders = await _dbContext.Orders.ToListAsync();

        return Result.Ok(_mapper.Map<IEnumerable<OrderResponse>>(orders));
    }

    public async Task<Result<OrderResponse>> CreateAsync(CreateOrderCommand createOrderCommand)
    {
        var customer = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == GuidConverter.Decode(createOrderCommand.ClientId!));

        if (customer is null)
        {
            return Result.Fail<OrderResponse>();
        }
        
        var order = _mapper.Map<Order>(createOrderCommand);
        
        order.Customer = customer;
        customer.CustomerOrders ??= new List<Order>();
        customer.CustomerOrders.Add(order);
        
        var deliveryPrice = DeliveryPriceHelper.CalculateDeliveryPrice(order.OrderType, order.Distance, order.OrderWeight);
        
        order.TotalPrice = deliveryPrice!.Value;

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return Result.Ok(_mapper.Map<OrderResponse>(order));
    }
}