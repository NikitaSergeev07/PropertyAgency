using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PropertyAgency.API.Contracts;
using PropertyAgency.API.repo;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.DAL.Repositories;
using Services.Implementations;
using Services.Interfaces;

namespace PropertyAgency.DAL.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("ApplicationDbContext"),
                b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
                ServiceLifetime.Scoped);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
            };
        });
        
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPropertiesRepository, PropertiesRepository>();
        services.AddScoped<IPropertiesService, PropertiesService>();
        services.AddScoped<IUser, UserRepo>();
        return services;
    }
}