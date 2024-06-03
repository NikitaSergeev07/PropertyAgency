namespace PropertyAgency.Domain.Entities;

public class Image
{
    public Guid Id { get; set; }
    public String ImageUrl { get; set; }
    
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    
}