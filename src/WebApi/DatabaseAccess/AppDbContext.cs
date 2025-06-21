using Microsoft.EntityFrameworkCore;
using WebApi.DatabaseAccess.Model;

namespace WebApi.DatabaseAccess;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();

    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(254);
                entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            }
        );

        modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Street).HasMaxLength(200);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.ZipCode).HasMaxLength(5);
            }
        );
    }
}
