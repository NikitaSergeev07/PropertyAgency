using System.ComponentModel.DataAnnotations;

namespace PropertyAgency.Domain.Enums;

public enum TypeProperty
{
    [Display(Name = "Аренда")]
    Rent = 0,
    [Display(Name = "Покупка")]
    Buy = 1
}