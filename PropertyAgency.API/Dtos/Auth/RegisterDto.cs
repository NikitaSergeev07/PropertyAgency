using System.ComponentModel.DataAnnotations;

namespace PropertyAgency.API.Dtos.Auth;

public class RegisterDto
{
    [Required] 
    public string UserName { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = String.Empty;
}