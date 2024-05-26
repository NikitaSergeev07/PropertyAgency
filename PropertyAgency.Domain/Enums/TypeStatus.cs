using System.ComponentModel.DataAnnotations;

namespace PropertyAgency.Domain.Enums;

public enum TypeStatus
{
    [Display(Name = "Куплено")]
    Bought = 0,
    [Display(Name = "В продаже")]
    InSale = 1
}