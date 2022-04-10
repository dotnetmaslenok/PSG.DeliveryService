using PSG.DeliveryService.Infrastructure.SeedHelpers;

namespace  PSG.DeliveryService.Api;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        await using (var scope = host.Services.CreateAsyncScope())
        {
            await SeedDatabaseHelper.SeedDatabaseAsync(scope.ServiceProvider);
        }
        
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

