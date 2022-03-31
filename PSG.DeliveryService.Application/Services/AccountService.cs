using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Helpers;
using PSG.DeliveryService.Application.Interfaces;
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

    private async Task<Result<JsonResult, IEnumerable<IdentityError>>> CreateUserAsync<TUser>(UserManager<TUser> userManager,
        SignInManager<TUser> signInManager,
        TUser user,
        Claim userClaim,
        string? password) where TUser : IdentityUser<Guid>
    {
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddClaimAsync(user, userClaim);
            
            var signInResult = await signInManager.PasswordSignInAsync(user, password, true, false);

            if (signInResult.Succeeded)
            {
                var token = AuthenticationHelper.SetBearerToken(user, new[] {userClaim}, _configuration);
                
                var userId = await _userManager.GetUserIdAsync((user as ApplicationUser)!);
                var response = new
                {
                    access_token = token,
                    currentUserId = userId
                };
                
                return Result.Ok<JsonResult, IEnumerable<IdentityError>>(new JsonResult(response));  
            }

            var identityError = new IdentityError
            {
                Description = ErrorMessages.WrongPasswordMessage
            };
            
            return Result.Fail<JsonResult, IEnumerable<IdentityError>>(new []{ identityError });
        }

        return Result.Fail<JsonResult, IEnumerable<IdentityError>>(result.Errors);
    }

    public async Task<Result<JsonResult, IEnumerable<IdentityError>>> CreateAsync(RegistrationCommand registrationCommand)
    {
        var isAlreadyExists = await _dbContext.Users.AnyAsync(x => x.PhoneNumber == registrationCommand.PhoneNumber);

        if (isAlreadyExists)
        {
            var phoneAlreadyExists = new IdentityError
            {
                Description = ErrorMessages.PhoneAlreadyExistsMessage
            };
            
            return Result.Fail<JsonResult, IEnumerable<IdentityError>>(new [] { phoneAlreadyExists });
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

    public async Task<Result<JsonResult, string>> SignInAsync(SignInCommand signUpCommand)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == signUpCommand.PhoneNumber);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, signUpCommand.Password, true, false);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
                };

                claims.Add(user.IsCourier ? UserClaims.CourierClaim : UserClaims.ClientClaim);

                var token = AuthenticationHelper.SetBearerToken(user, claims, _configuration);
                
                var userId = await _userManager.GetUserIdAsync(user);
                var response = new
                {
                    access_token = token,
                    currentUserId = userId
                };
                

                return Result.Ok<JsonResult, string>(new JsonResult(response));
            }
            
            return Result.Fail<JsonResult ,string>(ErrorMessages.WrongPasswordMessage);
        }
        
        return Result.Fail<JsonResult, string>(ErrorMessages.WrongLoginMessage);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}