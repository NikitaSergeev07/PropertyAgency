using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email);
    Task<List<Favorite>> GetFavoritesForUser(Guid id);
}