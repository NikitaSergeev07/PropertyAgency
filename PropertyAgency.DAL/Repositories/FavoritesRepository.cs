using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class FavoritesRepository : IFavoritesRepository
{
    private readonly ApplicationDbContext _context;

    public FavoritesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Favorite> Create(Favorite entity)
    {
        await _context.Favorites.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Favorite> GetById(Guid id)
    {
        return await _context.Favorites.AsNoTracking().Include(e => e.Property).Include(e => e.User).FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<List<Favorite>> Get()
    {
        return await _context.Favorites.AsNoTracking().Include(e => e.User).Include(e => e.Property).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Favorites.Where(p => p.Id == id).ExecuteDeleteAsync();
        return true;
    }


}