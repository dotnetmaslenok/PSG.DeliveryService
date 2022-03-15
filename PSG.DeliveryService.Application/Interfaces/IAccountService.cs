using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Application.Interfaces;

public interface IAccountService
{
    public Task SignInAsync<TUser>(TUser loginViewModel) where TUser : IdentityUser<int>;

    public Task RegisterAsync<TUser>(TUser registrationViewModel) where TUser : IdentityUser<int>;

    public Task SignOutAsync();
}