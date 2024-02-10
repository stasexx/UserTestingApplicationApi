using System.ComponentModel.Design;
using Application.IServices;
using Application.IServices.Identity;
using Infrastructure.Services;
using Infrastructure.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.InfrastructureExtentions;

public static class ServicesExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}