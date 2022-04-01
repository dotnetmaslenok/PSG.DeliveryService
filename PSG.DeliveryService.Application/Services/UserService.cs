using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Queries;
using PSG.DeliveryService.Application.Responses;
using PSG.DeliveryService.Infrastructure.Database;
using ResultMonad;

namespace PSG.DeliveryService.Application.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Result<UserResponse>> GetUserByIdAsync(UserQuery userQuery)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userQuery.Id);

        if (user is null)
        {
            return Result.Fail<UserResponse>();
        }

        var userResponse = _mapper.Map<UserResponse>(user);
        return Result.Ok(userResponse);
    }
}