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
    
    public async Task<List<Favorite>> GetFavoritesByUserId(Guid userId)
    {
        return await _favoritesRepository.GetFavoritesByUserId(userId);
    }

    public async Task<bool> DeleteFavorite(Guid id)
    {
        return await _favoritesRepository.Delete(id);
    }

    public async Task<Favorite> CreateFavorite(Favorite entity)
    {
        return await _favoritesRepository.Create(entity);
    }
    
}