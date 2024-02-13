using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;

namespace Tests.TestExtensions;

public class TestingFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    private bool _isDataInitialized = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(connection);
            });

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DataContext>();
                db.Database.EnsureCreated();
            }
            
            services.AddSingleton<SqliteConnection>(connection);
        });
    }

    public void InitializeDatabase()
    {
        if (_isDataInitialized) return;
        
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        DbInitializer.Initialize(dbContext);
        
        _isDataInitialized = true;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            
        }
        base.Dispose(disposing);
    }
}