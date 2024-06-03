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
        // Проверка, существует ли уже запись для данного пользователя и продукта
        var existingFavorite = await _context.Favorites
            .FirstOrDefaultAsync(fp => fp.UserId == entity.UserId && fp.PropertyId == entity.PropertyId);

        if (existingFavorite != null)
        {
            // Такая запись уже существует, значит продукт уже добавлен в избранное
            throw new InvalidOperationException("Продукт уже добавлен в избранное.");
        }
        await _context.Favorites.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
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