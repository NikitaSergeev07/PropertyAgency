using System.Text.Json.Serialization;
using PropertyAgency.Domain.Enums;

namespace PropertyAgency.Domain.Entities;

public class User
{

    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public TypeRole Role { get; set; } = TypeRole.User;
    
    public List<Favorite> Favorites { get; set; }
    public List<Rental> Rentals { get; set; }
<<<<<<< HEAD
    public List<Transaction> Transactions { get; set; }
=======
    public List<Operation> BuyerOperations { get; set; }
    public List<Operation> SellerOperations { get; set; }

>>>>>>> Nikita
    public User()
    {
        Favorites = new List<Favorite>();
        Rentals = new List<Rental>();
<<<<<<< HEAD
        Transactions = new List<Transaction>();
=======
        BuyerOperations = new List<Operation>();
        SellerOperations = new List<Operation>();
>>>>>>> Nikita
    }


}