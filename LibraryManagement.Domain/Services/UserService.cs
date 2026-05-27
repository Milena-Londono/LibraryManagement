using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // Retrieves all registered users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        // Retrieves a user by its identifier
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // Retrieves a user by email
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        // Creates a user after validating required fields and duplicate email
        public async Task<User> CreateAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new InvalidOperationException("User first name is required.");

            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new InvalidOperationException("User last name is required.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new InvalidOperationException("User email is required.");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new InvalidOperationException("User password is required.");

            var isEmailUnique = await _userRepository.IsEmailUniqueAsync(user.Email);

            if (!isEmailUnique)
                throw new InvalidOperationException("A user with the same email already exists.");

            _logger.LogInformation("Creating user with email {Email}", user.Email);

            return await _userRepository.AddAsync(user);
        }

        // Updates a user after validating existence and duplicate email
        public async Task UpdateAsync(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);

            if (existingUser is null)
                throw new InvalidOperationException("User not found.");

            var isEmailUnique = await _userRepository.IsEmailUniqueAsync(user.Email, user.Id);

            if (!isEmailUnique)
                throw new InvalidOperationException("A user with the same email already exists.");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Role = user.Role;
            existingUser.IsActive = user.IsActive;

            await _userRepository.UpdateAsync(existingUser);
        }

        // Deletes a user by its identifier
        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                throw new InvalidOperationException("User not found.");

            await _userRepository.DeleteAsync(user);
        }

        // Validates user credentials for authentication
        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                return null;

            if (!user.IsActive)
                return null;

            // Temporary validation for academic project.
            // Later this can be replaced by a real password hashing mechanism.
            if (user.PasswordHash != password)
                return null;

            return user;
        }
    }
}
