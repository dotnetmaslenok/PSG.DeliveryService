using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries.UserQueries;
using PSG.DeliveryService.Application.Responses;
using PSG.DeliveryService.Infrastructure.Database;
using ResultMonad;

namespace PSG.DeliveryService.Application.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CustomerService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Result<UserResponse>> GetByIdAsync(GetOneUserQuery getOneUserQuery)
    {
        var user = await _dbContext.Users
            .Include(x => x.CourierOrders)
            .Include(x => x.CustomerOrders)
            .FirstOrDefaultAsync(x => x.Id == getOneUserQuery.Id);

        if (user is null)
        {
            return Result.Fail<UserResponse>();
        }

        return Result.Ok(_mapper.Map<UserResponse>(user));
    }
}