using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
    }
}
