using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class RentalsRepository : IRentalsRepository
{
    private readonly ApplicationDbContext _context;

    public RentalsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Rental> Create(Rental entity)
    {
        // Валидация данных
        if (entity.RentAmount<=0)
        {
            throw new ArgumentException("У сданной хаты должна быть указана положительная цена");
        }
        if (entity.EndDate<=entity.StartDate)
        {
            throw new ArgumentException("Момент начала снимания хаты должен быть меньше окончания снимания хаты");
        }
        await _context.Rentals.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Rental> GetById(Guid id)
    {
        return await _context.Rentals.AsNoTracking().Include(e => e.User).Include(e => e.Property).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Rental>> Get()
    {
        return await _context.Rentals.AsNoTracking().
            Include(e => e.Property).Include(e => e.User).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Rentals.Where(p => p.Id == id).Include(e => e.User).Include(e => e.Property).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Rental entity)
    {
        await _context.Rentals
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Property)
            .Include(e => e.User)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.StartDate, p => entity.StartDate)
                .SetProperty(p => p.EndDate, p => entity.EndDate)
                .SetProperty(p => p.RentAmount, p => entity.RentAmount)
                .SetProperty(p => p.PropertyId, p => entity.PropertyId)
                .SetProperty(p => p.RenterId, p => entity.RenterId));
        return true;
    }
    public async Task<IEnumerable<Rental>> GetByBetweenDate(DateTime startDate, DateTime endDate)
    {
        return await _context.Rentals
            .AsNoTracking()
            .Include(e => e.Property)
            .Include(e => e.User)
            .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
            .ToListAsync();
    }
}