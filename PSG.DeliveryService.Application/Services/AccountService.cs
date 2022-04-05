using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Responses;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;
using ResultMonad;
using static PSG.DeliveryService.Application.Common.CustomConstants;

namespace PSG.DeliveryService.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext dbContext,
        IMapper mapper,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    private async Task<Result<AuthenticationResponse, IEnumerable<IdentityError>>> CreateUserAsync<TUser>(UserManager<TUser> userManager,
        SignInManager<TUser> signInManager,
        TUser user,
        Claim userClaim,
        string? password) where TUser : IdentityUser<Guid>
    {
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return Result.Fail<AuthenticationResponse, IEnumerable<IdentityError>>(result.Errors);
        }
        
        await userManager.AddClaimAsync(user, userClaim);
            
        var signInResult = await signInManager.PasswordSignInAsync(user, password, true, false);

        if (!signInResult.Succeeded)
        {
            var identityError = new IdentityError
            {
                Description = ErrorMessages.WrongPasswordMessage
            };
            
            return Result.Fail<AuthenticationResponse, IEnumerable<IdentityError>>(new []{ identityError });
        }
        
        var token = AuthenticationHelper.SetBearerToken(user, new[] {userClaim}, _configuration);
                
        var userId = await _userManager.GetUserIdAsync((user as ApplicationUser)!);

        var response = new AuthenticationResponse()
        {
            UserId = GuidConverter.Encode(Guid.Parse(userId)),
            AccessToken = token
        };
                
        return Result.Ok<AuthenticationResponse, IEnumerable<IdentityError>>(response);
    }

    public async Task<Result<AuthenticationResponse, IEnumerable<IdentityError>>> CreateAsync(RegistrationCommand registrationCommand)
    {
        var isAlreadyExists = await _dbContext.Users.AnyAsync(x => x.PhoneNumber == registrationCommand.PhoneNumber);

        if (isAlreadyExists)
        {
            var phoneAlreadyExists = new IdentityError
            {
                Description = ErrorMessages.PhoneAlreadyExistsMessage
            };
            
            return Result.Fail<AuthenticationResponse, IEnumerable<IdentityError>>(new [] { phoneAlreadyExists });
        }
        
        if (!registrationCommand.IsCourier)
        {
            var client = _mapper.Map<RegistrationCommand, ApplicationUser>(registrationCommand);

            var clientClaim = UserClaims.ClientClaim;
            return await CreateUserAsync(_userManager,
                _signInManager,
                client,
                clientClaim,
                registrationCommand.Password);
        }
        
        var courier = _mapper.Map<RegistrationCommand, ApplicationUser>(registrationCommand);

        var courierClaim = UserClaims.CourierClaim;
        return await CreateUserAsync(_userManager,
            _signInManager,
            courier,
            courierClaim,
            registrationCommand.Password);
    }

    public async Task<Result<AuthenticationResponse, string>> SignInAsync(SignInCommand signUpCommand)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == signUpCommand.PhoneNumber);

        if (user is null)
        {
            return Result.Fail<AuthenticationResponse, string>(ErrorMessages.WrongLoginMessage);
        }
        
        var result = await _signInManager.PasswordSignInAsync(user, signUpCommand.Password, true, false);

        if (!result.Succeeded)
        {
            return Result.Fail<AuthenticationResponse, string>(ErrorMessages.WrongPasswordMessage);
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.MobilePhone, user.PhoneNumber),
            user.IsCourier ? UserClaims.CourierClaim : UserClaims.ClientClaim
        };

        var token = AuthenticationHelper.SetBearerToken(user, claims, _configuration);
                
        var userId = await _userManager.GetUserIdAsync(user);

        var response = new AuthenticationResponse()
        {
            UserId = GuidConverter.Encode(Guid.Parse(userId)),
            AccessToken = token
        };

        return Result.Ok<AuthenticationResponse, string>(response);

    }
}