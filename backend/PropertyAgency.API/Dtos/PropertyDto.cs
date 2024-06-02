using PropertyAgency.Domain.Entities;
using PropertyAgency.Domain.Enums;

namespace PropertyAgency.API.Dtos;

public class PropertyDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int RoomCount { get; set; }
    public string Status { get; set; }
    public Guid UserId { get; set; }
    public List<FavoriteDto> Favorites { get; set; } = new List<FavoriteDto>();
}