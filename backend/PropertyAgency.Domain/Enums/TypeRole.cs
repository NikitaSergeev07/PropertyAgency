using System.ComponentModel.DataAnnotations;

namespace PropertyAgency.Domain.Enums;

public enum TypeRole
{
    [Display(Name = "Пользователь")]
    User = 0,
    [Display(Name = "Модератор")]
    Admin = 1
}