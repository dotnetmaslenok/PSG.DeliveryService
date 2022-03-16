using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PSG.DeliveryService.Domain.Authorization;

namespace PSG.DeliveryService.Api;

public class Startup
{
    public IConfiguration Configuration { get; set; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

    public void ConfigureServices(IServiceCollection services)
    {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo {Title = "DeliveryService", Version = "v1"});
            });

            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("DeliveryService"));
            });

            Action<IdentityOptions> sharedIdentityConfig = (config) =>
            {
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
                config.Password.RequiredUniqueChars = 0;
            };
            
            services.AddIdentityCore<ApplicationUser>(sharedIdentityConfig)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityCore<Courier>(sharedIdentityConfig)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Administrator",
                    policy =>
                    {
                        policy.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Administrator"));
                    });

                config.AddPolicy("Courier",
                    policy => { policy.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Courier")); });

                config.AddPolicy("Client",
                    policy => { policy.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Client")); });
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/api/user/Login";
                config.AccessDeniedPath = "/Account/AccessDenied";
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseCors(config =>
        {
            config.AllowAnyOrigin();
            config.AllowAnyMethod();
            config.AllowAnyHeader();
        });

        app.UseHttpsRedirection();
    
        app.UseAuthorization();
    
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}