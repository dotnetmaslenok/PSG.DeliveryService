using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Infrastructure.SeedHelpers;

public static class SeedDatabaseHelper
{
    public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        
        if (!await dbContext.Users.AnyAsync())
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var clientClaim = new Claim(ClaimTypes.Role, "Client");
            var courierClaim = new Claim(ClaimTypes.Role, "Courier");
            
            var client = new ApplicationUser()
            {
                UserName = "HaterJS228",
                Email = "a.kudryavcev0812@bk.ru",
                PhoneNumber = "+7-906-614-32-73",
            };

            var clientResult = await userManager.CreateAsync(client, "PasswordHaterJs228");

            if (clientResult.Succeeded)
            {
                await userManager.AddClaimAsync(client, clientClaim);
            }

            var courier = new ApplicationUser()
            {
                UserName = "JSEnjoyer2004",
                Email = "ebanatik@mail.ru",
                PhoneNumber = "+7-904-123-42-32"
            };

            var courierResult = await userManager.CreateAsync(courier, "PasswordJSEnjoyer2004");

            if (courierResult.Succeeded)
            {
                await userManager.AddClaimAsync(courier, courierClaim);
            }
        }
    }
}