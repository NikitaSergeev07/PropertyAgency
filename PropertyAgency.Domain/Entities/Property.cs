using System.ComponentModel.DataAnnotations.Schema;
using PropertyAgency.Domain.Enums;

namespace PropertyAgency.Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int RoomCount { get; set; }
    public string Status { get; set; }
    
    public Guid AddressId { get; set; }
    public Address Address { get; set; }

    public List<Favorite> Favorites { get; set; }
    public List<Rental> Rentals { get; set; }
<<<<<<< HEAD
    public List<Transaction> Transactions { get; set; }
    public List<Image> Images { get; set; }
=======
    
    public List<Image> Images { get; set; }
    
    public List<Operation> Operations { get; set; }
>>>>>>> Nikita

    public Property()
    {
        Favorites = new List<Favorite>();
        Rentals = new List<Rental>();
<<<<<<< HEAD
        Transactions = new List<Transaction>();
        Images = new List<Image>();
=======
        Images = new List<Image>();
        Operations = new List<Operation>();
>>>>>>> Nikita
    }
    
}