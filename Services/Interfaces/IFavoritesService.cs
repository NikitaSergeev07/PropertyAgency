using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IFavoritesService
{
    Task<List<Favorite>> GetFavoritesByUserId(Guid userId);
    Task<bool> DeleteFavorite(Guid id);
    Task<Favorite> CreateFavorite(Favorite entity);
    
}