using PropertyAgency.Domain.Entities;


namespace Services.Interfaces;

public interface IUsersService
{
    Task<IEnumerable<User>> GetUsers();

    Task<User> GetById(Guid id);
    Task<bool> DeleteUser(Guid id);
    Task<User> CreateUser(User entity);
    Task<bool> UpdateUser(User entity);
    Task<User> GetByEmail(string email);
    

}