using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class OperationsRepository : IOperationsRepository
{
    private readonly ApplicationDbContext _context;

    public OperationsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Operation> Create(Operation entity)
    {
        // Валидация данных
        if (entity.OperationDate == DateTime.MinValue)
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
        if (await _context.Operations.AnyAsync(t => t.Id == entity.Id))
        {
            throw new ArgumentException("Транзакция с указанным идентификатором уже существует.");
        }
        await _context.Operations.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Operation> GetById(Guid id)
    {
        return await _context.Operations.AsNoTracking().Include(e => e.Seller)
            .Include(e => e.Property).Include(e=> e.Buyer)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Operation>> Get()
    {
        return await _context.Operations.AsNoTracking().Include(e => e.Seller).
            Include(e => e.Property).Include(e=> e.Buyer).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Operations.Where(p => p.Id == id).Include(e => e.Seller).Include(e=> e.Buyer).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Operation entity)
    {
        await _context.Operations
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Seller)
            .Include(e => e.Property)
            .Include(e => e.Buyer)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.PropertyId, p => entity.PropertyId)
                .SetProperty(p => p.BuyerId, p => entity.BuyerId)
                .SetProperty(p => p.SellerId, p => entity.SellerId)
                .SetProperty(p => p.OperationDate, p => entity.OperationDate)
                .SetProperty(p => p.Amount, p => entity.Amount));
        return true;
    }
    public async Task<List<Operation>> GetByDate(DateTime OperationDate)
    {
        return await _context.Operations
            .AsNoTracking()
            .Include(e => e.Seller)
            .Include(e => e.Property)
            .Include(e => e.Buyer)
            .Where(r => r.OperationDate == OperationDate)
            .ToListAsync();
    }
    public async Task<List<Operation>> GetByAmount(Decimal OperationAmount)
    {
        return await _context.Operations
            .AsNoTracking()
            .Include(e => e.Seller)
            .Include(e => e.Property)
            .Include(e => e.Buyer)
            .Where(r => r.Amount == OperationAmount)
            .ToListAsync();
    }
}