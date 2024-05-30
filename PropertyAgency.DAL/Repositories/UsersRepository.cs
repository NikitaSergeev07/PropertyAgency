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
                .Include(u => u.Properties)
                .Include(u => u.Favorites)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Properties)
                .Include(u => u.Favorites)
                .ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Properties)
                .Include(u => u.Favorites)
                .ExecuteDeleteAsync();
            return true;
        }

        public async Task<bool> Update(User entity)
        {
            await _context.Users
                .Where(u => u.Id == entity.Id)
                .Include(u => u.Favorites)
                .Include(u => u.Properties)
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
                .Include(u => u.Properties)
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<List<Favorite>> GetFavoritesForUser(Guid userId)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(u => u.Properties)
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user.Favorites;
        }
    }
}
