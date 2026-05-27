using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves a user by email for login and authentication
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Validates whether the user email already exists
        public async Task<bool> IsEmailUniqueAsync(
            string email,
            int? excludeUserId = null)
        {
            return !await _context.Users
                .AnyAsync(u =>
                    u.Email == email &&
                    (!excludeUserId.HasValue || u.Id != excludeUserId.Value));
        }
    }
}
