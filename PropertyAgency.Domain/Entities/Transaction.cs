namespace PropertyAgency.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    
    public Guid BuyerId { get; set; }
    public User Buyer { get; set; }
    
    public Guid SellerId { get; set; }
    public User Seller { get; set; }
}