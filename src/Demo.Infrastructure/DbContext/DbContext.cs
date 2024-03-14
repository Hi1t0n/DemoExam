using Demo.Domain.Models;
using Demo.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Statement> Statements => Set<Statement>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Status> Statuses => Set<Status>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StatementConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new StatusConfiguration());
    }
}