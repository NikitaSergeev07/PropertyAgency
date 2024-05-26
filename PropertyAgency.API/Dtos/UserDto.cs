using PropertyAgency.Domain.Enums;

namespace PropertyAgency.API.Dtos;

public class UserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public TypeRole Role { get; set; }

    public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
}