using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<Option> Options { get; set; }
    
    public DbSet<UserTest> UserTests { get; set; }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
}