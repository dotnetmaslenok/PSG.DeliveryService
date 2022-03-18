using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Application.ExceptionHandling;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AccountService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private async Task<TResult> CreateUserAsync<TUser>(UserManager<TUser> userManager,
        SignInManager<TUser> signInManager,
        TUser user,
        Claim userClaim,
        string? password) where TUser : IdentityUser<int>
    {
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddClaimAsync(user, userClaim);
            await signInManager.PasswordSignInAsync(user, password, true, false);

            return new TResult(true);
        }

        return new TResult(false);
    }

    public async Task<TResult> SignUpAsync(SignUpViewModel signUpViewModel)
    {
        var isAlreadyExists = await _dbContext.Users.AnyAsync(x => x.PhoneNumber == signUpViewModel.PhoneNumber);

        if (isAlreadyExists)
        {
            return new TResult(false);
        }
        
        if (!signUpViewModel.IsCourier)
        {
            var client = _mapper.Map<SignUpViewModel, ApplicationUser>(signUpViewModel);

            var clientClaim = new Claim(ClaimTypes.Role, "Client");
            var result = await CreateUserAsync(_userManager,
                _signInManager,
                client, clientClaim,
                signUpViewModel.Password);

            if (result.Succeded)
            {
                return result;
            }
        }
        
        var courier = _mapper.Map<SignUpViewModel, ApplicationUser>(signUpViewModel);

        var courierClaim = new Claim(ClaimTypes.Role, "Courier");
        return await CreateUserAsync(_userManager,
            _signInManager,
            courier,
            courierClaim,
            signUpViewModel.Password);
    }

    public async Task<TResult> SignInAsync(SignInViewModel signUpViewModel)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == signUpViewModel.PhoneNumber);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, signUpViewModel.Password, true, false);

            if (result.Succeeded)
            {
                return new TResult(true);
            }
        }
        
        return new TResult(false);
    }

    public async Task<TResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();

        return new TResult(true);
    }
}