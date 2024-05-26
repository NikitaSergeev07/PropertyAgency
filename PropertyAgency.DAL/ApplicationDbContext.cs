using Microsoft.EntityFrameworkCore;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>()
            .HasOne(_ => _.User)
            .WithMany(_ => _.Properties)
            .HasForeignKey(_ => _.UserId);
    }
}