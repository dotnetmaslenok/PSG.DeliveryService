using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;
using PSG.DeliveryService.Infrastructure.SeedHelpers;

namespace  PSG.DeliveryService.Api;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // await using (var scope = host.Services.CreateAsyncScope())
        // {
        //     await scope.ServiceProvider.MigrateAsync();
        // }
        
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

