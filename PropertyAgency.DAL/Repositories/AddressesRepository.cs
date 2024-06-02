using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class AddressesRepository : IAddressesRepository
{
    private readonly ApplicationDbContext _context;

    public AddressesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Address> Create(Address entity)
    {
        await _context.Addresses.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Address> GetById(Guid id)
    {
        return await _context.Addresses.AsNoTracking().Include(e => e.Properties).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Address>> Get()
    {
        return await _context.Addresses.AsNoTracking().
            Include(e => e.Properties).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Addresses.Where(p => p.Id == id).Include(e => e.Properties).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Address entity)
    {
        await _context.Addresses
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Properties)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Street, p => entity.Street)
                .SetProperty(p => p.City, p => entity.City)
                .SetProperty(p => p.State, p => entity.State)
                .SetProperty(p => p.Country, p => entity.Country)
                .SetProperty(p => p.ZipCode, p => entity.ZipCode));
        return true;
    }


}