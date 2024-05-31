using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface ITransactionsRepository : IBaseRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetByDate(DateTime TransactionDate);
    Task<IEnumerable<Transaction>> GetByAmount(Decimal TransactionAmount);
}