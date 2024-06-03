using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Transaction> Create(Transaction entity)
    {
        // Валидация данных
        if (entity.TransactionDate == DateTime.MinValue)
        {
            throw new ArgumentException("Дата транзакции не может быть пустой.");
        }

        if (entity.Amount <= 0)
        {
            throw new ArgumentException("Сумма транзакции должна быть больше 0.");
        }

        // Проверка существования имущества, покупателя и продавца в БД
        if (!await _context.Properties.AnyAsync(p => p.Id == entity.PropertyId))
        {
            throw new ArgumentException("Имущество с указанным идентификатором не найдено.");
        }

        if (!await _context.Users.AnyAsync(b => b.Id == entity.BuyerId))
        {
            throw new ArgumentException("Покупатель с указанным идентификатором не найден.");
        }

        if (!await _context.Users.AnyAsync(s => s.Id == entity.SellerId))
        {
            throw new ArgumentException("Продавец с указанным идентификатором не найден.");
        }

        // Проверка уникальности идентификатора транзакции
        if (await _context.Transactions.AnyAsync(t => t.Id == entity.Id))
        {
            throw new ArgumentException("Транзакция с указанным идентификатором уже существует.");
        }
        await _context.Transactions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Transaction> GetById(Guid id)
    {
        return await _context.Transactions.AsNoTracking().Include(e => e.Seller)
            .Include(e => e.Property).Include(e=> e.Buyer)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Transaction>> Get()
    {
        return await _context.Transactions.AsNoTracking().Include(e => e.Seller).
            Include(e => e.Property).Include(e=> e.Buyer).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Transactions.Where(p => p.Id == id).Include(e => e.Seller).
            Include(e => e.Property).Include(e=> e.Buyer).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Transaction entity)
    {
        await _context.Transactions
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Seller)
            .Include(e => e.Property)
            .Include(e => e.Buyer)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.PropertyId, p => entity.PropertyId)
                .SetProperty(p => p.BuyerId, p => entity.BuyerId)
                .SetProperty(p => p.SellerId, p => entity.SellerId)
                .SetProperty(p => p.TransactionDate, p => entity.TransactionDate)
                .SetProperty(p => p.Amount, p => entity.Amount));
        return true;
    }
    public async Task<IEnumerable<Transaction>> GetByDate(DateTime TransactionDate)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Include(e => e.Seller)
            .Include(e => e.Property)
            .Include(e => e.Buyer)
            .Where(r => r.TransactionDate == TransactionDate)
            .ToListAsync();
    }
    public async Task<IEnumerable<Transaction>> GetByAmount(Decimal TransactionAmount)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Include(e => e.Seller)
            .Include(e => e.Property)
            .Include(e => e.Buyer)
            .Where(r => r.Amount == TransactionAmount)
            .ToListAsync();
    }
}