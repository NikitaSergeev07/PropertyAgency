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

    public List<Property> Properties { get; set; }

    public User()
    {
        Properties = new List<Property>();
    }


}