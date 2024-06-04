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
        return await _context.Properties.AsNoTracking().Include(e => e.Operations).Include(e => e.Rentals).Include(e => e.Favorites).Include(e => e.Images).Include(e => e.Address).FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<List<Property>> Get()
    {
        return await _context.Properties.AsNoTracking().Include(e => e.Operations).Include(e => e.Rentals).Include(e => e.Favorites).Include(e => e.Address).Include(e => e.Images).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Properties.Where(p => p.Id == id).Include(e => e.Operations).Include(e => e.Rentals).Include(e => e.Favorites).Include(e => e.Images).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Property entity)
    {
        await _context.Properties
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Operations)
            .Include(e => e.Favorites)
            .Include(e => e.Address)
            .Include(e => e.Rentals)
            .Include(e => e.Images)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Title, p => entity.Title)
                .SetProperty(p => p.Description, p => entity.Description)
                .SetProperty(p => p.Price, p => entity.Price)
                .SetProperty(p => p.RoomCount, p => entity.RoomCount)
                .SetProperty(p => p.Status, p => entity.Status)
                .SetProperty(p => p.AddressId, p => entity.AddressId));
        return true;
    }
    
    public async Task<List<Property>> GetFilteredProperties(List<int> rooms, decimal? priceMin, decimal? priceMax, string street = null, string city = null, string state = null, string country = null, string zipCode = null)
    {
        IQueryable<Property> query = _context.Properties.Include(e => e.Rentals)
                                                         .Include(e => e.Favorites)
                                                         .Include(e => e.Address)
                                                         .Include(e => e.Images)
                                                         .Include(e => e.Operations);

        if (rooms != null && rooms.Any())
        {
            if (rooms.Contains(6))
            {
                query = query.Where(p => p.RoomCount >= 6 || rooms.Contains(p.RoomCount) && p.RoomCount < 6);
            }
            else
            {
                query = query.Where(p => rooms.Contains(p.RoomCount));
            }
        }

        if (priceMin.HasValue && priceMax.HasValue && priceMin.Value >= 0 && priceMax.Value > priceMin.Value)
        {
            query = query.Where(p => p.Price >= priceMin.Value && p.Price <= priceMax.Value);
        }
        else if (priceMin.HasValue && priceMin.Value >= 0)
        {
            query = query.Where(p => p.Price >= priceMin.Value);
        }
        else if (priceMax.HasValue && priceMax.Value >= 0)
        {
            query = query.Where(p => p.Price <= priceMax.Value);
        }

        if (!string.IsNullOrEmpty(street))
        {
            query = query.Where(p => p.Address.Street.Contains(street));
        }

        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(p => p.Address.City.Contains(city));
        }

        if (!string.IsNullOrEmpty(state))
        {
            query = query.Where(p => p.Address.State.Contains(state));
        }

        if (!string.IsNullOrEmpty(country))
        {
            query = query.Where(p => p.Address.Country.Contains(country));
        }

        if (!string.IsNullOrEmpty(zipCode))
        {
            query = query.Where(p => p.Address.ZipCode.Contains(zipCode));
        }

        return await query.AsNoTracking().ToListAsync();
    }

}