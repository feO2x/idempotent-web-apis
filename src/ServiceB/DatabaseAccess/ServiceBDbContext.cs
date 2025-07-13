using Microsoft.EntityFrameworkCore;
using ServiceB.DatabaseAccess.Model;

namespace ServiceB.DatabaseAccess;

public sealed class ServiceBDbContext : DbContext
{
    public ServiceBDbContext(DbContextOptions<ServiceBDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Email).IsRequired(false).HasMaxLength(100);
            entity.Property(x => x.PhoneNumber).IsRequired(false).HasMaxLength(100);
        });
    }
}