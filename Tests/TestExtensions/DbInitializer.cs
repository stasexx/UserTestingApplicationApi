using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Database; // Додайте using для доступу до DataContext

namespace Tests.TestExtensions;

public class TestingFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    private bool _isDataInitialized = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Знайдіть існуючий дескриптор для DataContext та видаліть його
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<DataContext>)); // Змінено на DataContext

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            // Додайте DataContext з використанням SQLite
            services.AddDbContext<DataContext>(options => // Змінено на DataContext
            {
                options.UseSqlite("Filename=TestDatabase.db");
            });
        });
    }

    public void InitializeDatabase()
    {
        if (_isDataInitialized) return;

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>(); // Використовуйте DataContext
        
        // Створіть базу даних, якщо вона ще не існує
        dbContext.Database.EnsureCreated();

        _isDataInitialized = true;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Очищення ресурсів, якщо потрібно
        }
        base.Dispose(disposing);
    }
}