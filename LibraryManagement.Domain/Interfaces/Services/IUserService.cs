using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface IUserService
    {
        // Retrieves all users
        Task<IEnumerable<User>> GetAllAsync();

        // Retrieves a user by its identifier
        Task<User?> GetByIdAsync(int id);

        // Retrieves a user by email
        Task<User?> GetByEmailAsync(string email);

        // Creates a new user after business validations
        Task<User> CreateAsync(User user);

        // Updates an existing user after business validations
        Task UpdateAsync(User user);

        // Deletes a user by its identifier
        Task DeleteAsync(int id);

        // Validates user credentials for authentication
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
