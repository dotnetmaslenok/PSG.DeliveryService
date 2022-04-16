using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Infrastructure.SeedHelpers;

public static class MigrationHelper
{
    public static async Task MigrateAsync(this IServiceProvider serviceProvider)
    {
        await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Database.IsNpgsql())
        {
            await context.Database.MigrateAsync();
        }

        await context.SeedDatabaseAsync(serviceProvider);
    }
}