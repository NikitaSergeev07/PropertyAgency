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

    public Task<Rental> GetByBetweenDate(Guid id)
    {
        //сделать
        throw new NotImplementedException();
    }
}