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
    
    public async Task<Guid> Create(Favorite entity)
    {
        await _context.Favorites.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
    

    public async Task<List<Favorite>> GetFavoritesByUserId(Guid userId)
    {
        return await _context.Favorites.AsNoTracking()
            .Include(e => e.Property)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Favorites.Where(p => p.Id == id).ExecuteDeleteAsync();
        return true;
    }


}