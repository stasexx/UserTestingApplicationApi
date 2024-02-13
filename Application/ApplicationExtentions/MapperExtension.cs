using System.Reflection;
using Application.MappingProfile;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ApplicationExtentions;

public static class MapperExtension
{ 
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TestProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(QuestionProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(OptionProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(UserTestProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));

        return services;
    }
}