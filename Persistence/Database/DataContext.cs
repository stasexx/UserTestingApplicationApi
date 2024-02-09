using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<User> Tests { get; set; }
    
    public DbSet<User> Questions { get; set; }
    
    public DbSet<User> UserTests { get; set; }
    
}