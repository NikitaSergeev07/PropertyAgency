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
    public DbSet<Image> Images { get; set; }
    
    public DbSet<Operation> Operations { get; set; }

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

        modelBuilder.Entity<Image>()
            .HasOne(_ => _.Property)
            .WithMany(_ => _.Images)
            .HasForeignKey(_ => _.PropertyId);

        modelBuilder.Entity<Operation>()
            .HasOne(_ => _.Property)
            .WithMany(_ => _.Operations)
            .HasForeignKey(_ => _.PropertyId);
        
        modelBuilder.Entity<Operation>()
            .HasOne(_ => _.Property)
            .WithMany(_ => _.Operations)
            .HasForeignKey(_ => _.PropertyId);

        modelBuilder.Entity<Operation>()
            .HasOne(_ => _.Buyer)
            .WithMany(_ => _.BuyerOperations)
            .HasForeignKey(_ => _.BuyerId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Operation>()
            .HasOne(_ => _.Seller)
            .WithMany(_ => _.SellerOperations)
            .HasForeignKey(_ => _.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    
}