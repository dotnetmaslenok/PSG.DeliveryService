using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.Services;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Infrastructure.Database;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.PipelineBehaviors;
using PSG.DeliveryService.Application.Profiles;
using PSG.DeliveryService.Application.Validation.AccountValidators;
using PSG.DeliveryService.Application.Validation.BaseValidators;

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
        services.AddEndpointsApiExplorer();

        services.AddControllers();

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo {Title = "DeliveryService", Version = "v1"});
        });

        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseSqlServer(Configuration.GetConnectionString("DeliveryService"));
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
                config.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(config => config.SlidingExpiration = true)
            .AddJwtBearer(config =>
            {
                var secretKey = Configuration["BearerSalt"];
                var secretBytes = Encoding.UTF8.GetBytes(secretKey);
                var key = new SymmetricSecurityKey(secretBytes);

                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = Configuration["Authentication:Bearer:Issuer"],
                    ValidAudience = Configuration["Authentication:Bearer:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = key
                };
            });

        services.AddAuthorization(config =>
        {
            config.AddPolicy("Bearer", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            config.AddPolicy("Administrator",
                policy =>
                {
                    policy.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Administrator"));
                });

            config.AddPolicy("Courier",
                policy =>
                {
                    policy.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Courier"));
                });

            config.AddPolicy("Client",
                policy =>
                {
                    policy.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Client"));
                });
        });

        services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/api/account/sign-in";
            config.AccessDeniedPath = "/api/account/accessDenied";
            config.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    if (context.Request.Method != "GET")
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                        return Task.CompletedTask;
                    }

                    context.RedirectUri = context.Options.LoginPath;
                    return Task.CompletedTask;
                }
            };
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICourierService, CourierService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();

        services.AddMediatR(typeof(RegistrationCommand).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddAutoMapper(typeof(MappingProfile));
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

        app.UseAuthentication();
    
        app.UseAuthorization();
    
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}