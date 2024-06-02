namespace PropertyAgency.API.Dtos;

public class RentalDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal RentAmount { get; set; }
    
    public Guid PropertyId { get; set; }
    
    public Guid RenterId { get; set; }
}