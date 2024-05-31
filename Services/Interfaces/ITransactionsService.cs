using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface ITransactionsService
{
    Task<IEnumerable<Transaction>> GetTransactions();

    Task<Transaction> GetById(Guid id);
    Task<bool> DeleteTransaction(Guid id);
    Task<Transaction> CreateTransaction(Transaction entity);
    Task<bool> UpdateTransaction(Transaction entity);

    Task<IEnumerable<Transaction>> GetTransactionsByDate(DateTime Date);
    Task<IEnumerable<Transaction>> GetTransactionsByAmount(decimal Amount);
}