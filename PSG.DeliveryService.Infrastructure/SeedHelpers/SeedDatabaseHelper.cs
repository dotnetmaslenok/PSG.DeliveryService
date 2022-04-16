using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Infrastructure.SeedHelpers;

public static class SeedDatabaseHelper
{
    public static async Task SeedDatabaseAsync(this ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var clientClaim = new Claim(ClaimTypes.Role, "Client");
            var courierClaim = new Claim(ClaimTypes.Role, "Courier");
            var orderManagerClaim = new Claim(ClaimTypes.Role, "OrderManager");
            
            var client = new ApplicationUser()
            {
                UserName = "ClientUserName",
                PhoneNumber = "+7-(111)-111-11-11",
                PhoneNumberConfirmed = true
            };

            var clientResult = await userManager.CreateAsync(client, "ClientPassword111");

            if (clientResult.Succeeded)
            {
                await userManager.AddClaimAsync(client, clientClaim);
            }

            var courier = new ApplicationUser()
            {
                UserName = "CourierUserName",
                PhoneNumber = "+7-(222)-222-22-22",
                PhoneNumberConfirmed = true,
                IsCourier = true
            };

            var courierResult = await userManager.CreateAsync(courier, "CourierPassword222");

            if (courierResult.Succeeded)
            {
                await userManager.AddClaimAsync(courier, courierClaim);
            }

            var orderManager = new ApplicationUser()
            {
                UserName = "OrderManagerUserName",
                PhoneNumber = "+7-(333)-333-33-33",
                PhoneNumberConfirmed = true
            };

            var orderManagerResult = await userManager.CreateAsync(orderManager, "OrderManagerPassword333");

            if (orderManagerResult.Succeeded)
            {
                await userManager.AddClaimAsync(orderManager, orderManagerClaim);
            }
        }
    }
}