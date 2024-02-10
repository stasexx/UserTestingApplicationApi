using Application.IRepositories;
using Application.IRepositories.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;
using Persistence.Repositories;
using Persistence.Repositories.Identity;

namespace Persistence.PersistenceExtension;

public static class PersistenceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IOptionRepository, OptionRepository>();
        services.AddScoped<IUserTestRepository, UserTestRepository>();
        
        return services;
    }
}