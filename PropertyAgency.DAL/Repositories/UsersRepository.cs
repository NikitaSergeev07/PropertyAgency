using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .ExecuteDeleteAsync();
            return true;
        }

        public async Task<bool> Update(User entity)
        {
            await _context.Users
                .Where(u => u.Id == entity.Id)
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.UserName, entity.UserName)
                    .SetProperty(u => u.Email, entity.Email)
                    .SetProperty(u => u.Password, entity.Password)
                    .SetProperty(u => u.Role, entity.Role));
            return true;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        
    }
}
