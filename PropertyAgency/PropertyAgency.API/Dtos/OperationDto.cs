namespace PropertyAgency.API.Dtos;

public class OperationDto
{
    public DateTime OperationDate { get; set; }
    public decimal Amount { get; set; }
    
    public Guid PropertyId { get; set; }
    
    public Guid SellerId { get; set; }
    
    public Guid BuyerId { get; set; }
}