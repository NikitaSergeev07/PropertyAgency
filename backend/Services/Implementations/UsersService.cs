using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using PropertyAgency.Domain.Enums;

using Services.Interfaces;

namespace Services.Implementations;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<IEnumerable<User>> GetUsers()
    {
        var users = await _usersRepository.Get();
        return users;
    }

    public async Task<User> GetById(Guid id)
    {
        return await _usersRepository.GetById(id);
    }
    
    public async Task<User> CreateUser(User entity)
    {
        return await _usersRepository.Create(entity);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        return await _usersRepository.Delete(id);
    }
    public async Task<bool> UpdateUser(User entity)
    {
        return await _usersRepository.Update(entity);
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _usersRepository.GetByEmail(email);
    }


}