using System.ComponentModel.DataAnnotations.Schema;
using PropertyAgency.Domain.Enums;

namespace PropertyAgency.Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public TypeProperty TypeProperty { get; set; }
    public TypeStatus Status { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
}