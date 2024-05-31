using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class TransactionsService: ITransactionsService
{
    private readonly ITransactionsRepository _transactionsRepository;

    public TransactionsService(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<IEnumerable<Transaction>> GetTransactions()
    {
        return await _transactionsRepository.Get();
    }

    public async Task<Transaction> GetById(Guid id)
    {
        return await _transactionsRepository.GetById(id);
    }

    public async Task<bool> DeleteTransaction(Guid id)
    {
        return await _transactionsRepository.Delete(id);
    }

    public async Task<Transaction> CreateTransaction(Transaction entity)
    {
        return await _transactionsRepository.Create(entity);
    }

    public async Task<bool> UpdateTransaction(Transaction entity)
    {
        return await _transactionsRepository.Update(entity);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByDate(DateTime Date)
    {
        return await _transactionsRepository.GetByDate(Date);
    }
    public async Task<IEnumerable<Transaction>> GetTransactionsByAmount(Decimal Amount)
    {
        return await _transactionsRepository.GetByAmount(Amount);
    }
}