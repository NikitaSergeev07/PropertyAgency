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
            // Валидация данных
            if (string.IsNullOrEmpty(entity.UserName))
            {
                throw new ArgumentException("Имя пользователя не может быть пустым.");
            }

            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentException("Почта не может быть пустой.");
            }

            if (string.IsNullOrEmpty(entity.Password))
            {
                throw new ArgumentException("Пароль не может быть пустым.");
            }

            if (string.IsNullOrEmpty(entity.Role.ToString()))
            {
                throw new ArgumentException("Роль не может быть пустой.");
            }

            // Проверка формата электронной почты
            if (!IsValidEmail(entity.Email))
            {
                throw new ArgumentException("Некорректный формат электронной почты.");
            }

            // Проверка уникальности имени пользователя и электронной почты
            if (await _context.Users.AnyAsync(u => u.UserName.ToLower() == entity.UserName.ToLower()))
            {
                throw new ArgumentException("Имя пользователя уже занято.");
            }

            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == entity.Email.ToLower()))
            {
                throw new ArgumentException("Электронная почта уже используется.");
            }
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        private bool IsValidEmail(string email)
        {
            // Простая проверка формата электронной почты
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }
        public async Task<User> GetById(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .Include(u => u.BuyerOperations)
                .Include(u => u.SellerOperations)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .Include(u => u.BuyerOperations)
                .Include(u => u.SellerOperations)
                .ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .Include(u => u.BuyerOperations)
                .Include(u => u.SellerOperations)
                .ExecuteDeleteAsync();
            return true;
        }

        public async Task<bool> Update(User entity)
        {
            await _context.Users
                .Where(u => u.Id == entity.Id)
                .Include(u => u.Favorites)
                .Include(u => u.Rentals)
                .Include(u => u.BuyerOperations)
                .Include(u => u.SellerOperations)
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
                .Include(u => u.BuyerOperations)
                .Include(u => u.SellerOperations)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        
    }
}
