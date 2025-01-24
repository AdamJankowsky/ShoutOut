using Microsoft.EntityFrameworkCore;
using ShoutOut.Database.Entities;

namespace ShoutOut.Database;

public class ShoutOutDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public ShoutOutDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoutOutDbContext).Assembly);
    }
}