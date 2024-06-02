namespace PropertyAgency.Domain.Entities;

public class Rental
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal RentAmount { get; set; }
    
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    
    public Guid RenterId { get; set; }
    public User User { get; set; }
}