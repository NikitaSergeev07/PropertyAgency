using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IFavoritesRepository
{
    Task<Favorite> Create(Favorite entity);
    Task<Favorite> GetById(Guid id);
    Task<List<Favorite>> Get();
    Task<bool> Delete(Guid id);
}