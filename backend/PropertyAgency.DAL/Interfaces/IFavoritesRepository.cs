using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IFavoritesRepository
{
    Task<Guid> Create(Favorite entity);
    Task<List<Favorite>> GetFavoritesByUserId(Guid userId);
    Task<bool> Delete(Guid id);
}