using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class FavoritesService : IFavoritesService
{
    private readonly IFavoritesRepository _favoritesRepository;
    private readonly IPropertiesRepository _propertiesRepository;

    public FavoritesService(IFavoritesRepository favoritesRepository, IPropertiesRepository propertiesRepository)
    {
        _favoritesRepository = favoritesRepository;
        _propertiesRepository = propertiesRepository;
    }
    
    public async Task<IEnumerable<Favorite>> GetFavorites()
    {
        var favorites = await _favoritesRepository.Get();
        return favorites;
    }

    public async Task<Favorite> GetById(Guid id)
    {
        return await _favoritesRepository.GetById(id);
    }

    public async Task<bool> DeleteFavorite(Guid id)
    {
        return await _favoritesRepository.Delete(id);
    }

    public async Task<Favorite> CreateFavorite(Favorite entity)
    {
        var property = await _propertiesRepository.GetById(entity.PropertyId);
        if (property == null || property.UserId != entity.UserId)
        {
            throw new InvalidOperationException("Объект не принадлежит этому пользователю");
        }

        return await _favoritesRepository.Create(entity);
    }
    

    
    
}