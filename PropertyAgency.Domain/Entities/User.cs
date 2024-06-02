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
    public User()
    {
        Favorites = new List<Favorite>();
        Rentals = new List<Rental>();
    }


}