using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly ApplicationDbContext _context;

    public PropertiesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Property> Create(Property entity)
    {
        await _context.Properties.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Property> GetById(Guid id)
    {
        return await _context.Properties.AsNoTracking().Include(e => e.Rentals).Include(e => e.User).Include(e => e.Favorites).Include(e => e.Address).FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<List<Property>> Get()
    {
        return await _context.Properties.AsNoTracking().Include(e => e.Rentals).Include(e => e.User).Include(e => e.Favorites).Include(e => e.Address).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Properties.Where(p => p.Id == id).Include(e => e.Rentals).Include(e => e.Favorites).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Property entity)
    {
        await _context.Properties
            .Where(p => p.Id == entity.Id)
            .Include(e => e.User)
            .Include(e => e.Favorites)
            .Include(e => e.Address)
            .Include(e => e.Rentals)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Title, p => entity.Title)
                .SetProperty(p => p.Description, p => entity.Description)
                .SetProperty(p => p.Price, p => entity.Price)
                .SetProperty(p => p.RoomCount, p => entity.RoomCount)
                .SetProperty(p => p.Status, p => entity.Status)
                .SetProperty(p => p.UserId, p => entity.UserId));
        return true;
    }
}