namespace PropertyAgency.Domain.Entities;

public class Favorite
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    
}