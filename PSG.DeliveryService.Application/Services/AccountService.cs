using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSG.DeliveryService.Application.Authentication;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;
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

    private async Task<Result<string, IEnumerable<IdentityError>>> CreateUserAsync<TUser>(UserManager<TUser> userManager,
        SignInManager<TUser> signInManager,
        TUser user,
        Claim userClaim,
        string? password) where TUser : IdentityUser<int>
    {
        var result = await userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            await userManager.AddClaimAsync(user, userClaim);
            
            var signInResult = await signInManager.PasswordSignInAsync(user, password, true, false);

            if (signInResult.Succeeded)
            {
                var token = AuthenticationHelper.SetBearerToken(user, new[] {userClaim}, _configuration);
                
                return Result.Ok<string, IEnumerable<IdentityError>>(token);  
            }

            var identityError = new IdentityError
            {
                Description = ErrorMessages.WrongPasswordMessage
            };
            
            return Result.Fail<string, IEnumerable<IdentityError>>(new []{ identityError });
        }

        return Result.Fail<string, IEnumerable<IdentityError>>(result.Errors);
    }

    public async Task<Result<string, IEnumerable<IdentityError>>> SignUpAsync(SignUpViewModel signUpViewModel)
    {
        var isAlreadyExists = await _dbContext.Users.AnyAsync(x => x.PhoneNumber == signUpViewModel.PhoneNumber);

        if (isAlreadyExists)
        {
            var phoneAlreadyExists = new IdentityError
            {
                Description = ErrorMessages.PhoneAlreadyExistsMessage
            };
            
            return Result.Fail<string, IEnumerable<IdentityError>>(new [] { phoneAlreadyExists });
        }
        
        if (!signUpViewModel.IsCourier)
        {
            var client = _mapper.Map<SignUpViewModel, ApplicationUser>(signUpViewModel);

            var clientClaim = UserClaims.ClientClaim;
            return await CreateUserAsync(_userManager,
                _signInManager,
                client,
                clientClaim,
                signUpViewModel.Password);
        }
        
        var courier = _mapper.Map<SignUpViewModel, ApplicationUser>(signUpViewModel);

        var courierClaim = UserClaims.CourierClaim;
        return await CreateUserAsync(_userManager,
            _signInManager,
            courier,
            courierClaim,
            signUpViewModel.Password);
    }

    public async Task<Result<string, string>> SignInAsync(SignInViewModel signUpViewModel)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == signUpViewModel.PhoneNumber);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, signUpViewModel.Password, true, false);

            if (result.Succeeded)
            {
                var token = user.IsCourier
                    ? AuthenticationHelper.SetBearerToken(user, new[] {UserClaims.CourierClaim}, _configuration)
                    : AuthenticationHelper.SetBearerToken(user, new[] {UserClaims.ClientClaim}, _configuration);

                return Result.Ok<string, string>(token);
            }
            
            return Result.Fail<string ,string>(ErrorMessages.WrongPasswordMessage);
        }
        
        return Result.Fail<string, string>(ErrorMessages.WrongLoginMessage);
    }

    public async Task<Result> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        
        return Result.Ok();
    }
}