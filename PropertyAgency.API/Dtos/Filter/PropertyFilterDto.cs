namespace PropertyAgency.API.Dtos.Filter;

public class PropertyFilterDTO
{
    public List<int> Rooms { get; set; } = new List<int>();
    public decimal PriceMin { get; set; } = 0;
    public decimal PriceMax { get; set; } = Decimal.MaxValue;
    public string? Street { get; set; } = null;
    public string? City { get; set; } = null;
    public string? State { get; set; } = null;
    public string? Country { get; set; } = null;
    public string? ZipCode { get; set; } = null;
}