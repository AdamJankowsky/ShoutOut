using Microsoft.EntityFrameworkCore;
using ShoutOut.Database.Entities;

namespace ShoutOut.Database;

public class ShoutOutDbContext : DbContext
{
    public ShoutOutDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<BlogPost> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoutOutDbContext).Assembly);
        modelBuilder.UseIdentityByDefaultColumns();
    }
}