using PropertyAgency.Domain.Entities;
using PropertyAgency.Domain.Enums;

namespace PropertyAgency.API.Dtos;

public class PropertyDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public TypeProperty TypeProperty { get; set; }
    public TypeStatus Status { get; set; }
    public Guid UserId { get; set; }
}