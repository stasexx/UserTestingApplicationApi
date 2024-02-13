using System.ComponentModel.Design;
using Application.IRepositories;
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
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IOptionService, OptionService>();
        services.AddScoped<IUserTestService, UserTestService>();

        return services;
    }
}