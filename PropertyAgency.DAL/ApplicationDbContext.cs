using Microsoft.EntityFrameworkCore;
using PropertyAgency.Domain.Entities;
using Transaction = System.Transactions.Transaction;

namespace PropertyAgency.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Rental> Rentals { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Favorite>()
            .HasOne(_ => _.User)
            .WithMany(_ => _.Favorites)
            .HasForeignKey(_ => _.UserId);
        
        modelBuilder.Entity<Favorite>()
            .HasOne(_ => _.Property)
            .WithMany(_ => _.Favorites)
            .HasForeignKey(_ => _.PropertyId);

        modelBuilder.Entity<Property>()
            .HasOne(_ => _.Address)
            .WithMany(_ => _.Properties)
            .HasForeignKey(_ => _.AddressId);
        
        modelBuilder.Entity<Rental>()
            .HasOne(_ => _.User)
            .WithMany(_ => _.Rentals)
            .HasForeignKey(_ => _.RenterId);
        
        modelBuilder.Entity<Rental>()
            .HasOne(_ => _.Property)
            .WithMany(_ => _.Rentals)
            .HasForeignKey(_ => _.PropertyId);
    }
}