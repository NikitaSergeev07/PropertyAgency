using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IFavoritesService
{

    Task<Favorite> GetById(Guid id);
    Task<bool> DeleteFavorite(Guid id);
    Task<Favorite> CreateFavorite(Favorite entity);

    Task<IEnumerable<Favorite>> GetFavorites();
}