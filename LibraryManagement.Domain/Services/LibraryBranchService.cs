using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class LibraryBranchService : ILibraryBranchService
    {
        private readonly ILibraryBranchRepository _libraryBranchRepository;
        private readonly ILogger<LibraryBranchService> _logger;

        public LibraryBranchService(
            ILibraryBranchRepository libraryBranchRepository,
            ILogger<LibraryBranchService> logger)
        {
            _libraryBranchRepository = libraryBranchRepository;
            _logger = logger;
        }

        // Retrieves all registered library branches
        public async Task<IEnumerable<LibraryBranch>> GetAllAsync()
        {
            return await _libraryBranchRepository.GetAllAsync();
        }

        // Retrieves a library branch by its identifier
        public async Task<LibraryBranch?> GetByIdAsync(int id)
        {
            return await _libraryBranchRepository.GetByIdAsync(id);
        }

        // Retrieves library branches by city
        public async Task<IEnumerable<LibraryBranch>> GetByCityAsync(string city)
        {
            return await _libraryBranchRepository.GetByCityAsync(city);
        }

        // Creates a library branch after validating required fields and duplicate names
        public async Task<LibraryBranch> CreateAsync(LibraryBranch libraryBranch)
        {
            if (string.IsNullOrWhiteSpace(libraryBranch.Name))
                throw new InvalidOperationException("Library branch name is required.");

            if (string.IsNullOrWhiteSpace(libraryBranch.Address))
                throw new InvalidOperationException("Library branch address is required.");

            if (string.IsNullOrWhiteSpace(libraryBranch.City))
                throw new InvalidOperationException("Library branch city is required.");

            var isNameUnique = await _libraryBranchRepository
                .IsNameUniqueAsync(libraryBranch.Name);

            if (!isNameUnique)
                throw new InvalidOperationException("A library branch with the same name already exists.");

            _logger.LogInformation(
                "Creating library branch {LibraryBranchName}",
                libraryBranch.Name);

            return await _libraryBranchRepository.AddAsync(libraryBranch);
        }

        // Updates a library branch after validating existence and duplicate names
        public async Task UpdateAsync(LibraryBranch libraryBranch)
        {
            var existingBranch = await _libraryBranchRepository.GetByIdAsync(libraryBranch.Id);

            if (existingBranch is null)
                throw new InvalidOperationException("Library branch not found.");

            var isNameUnique = await _libraryBranchRepository
                .IsNameUniqueAsync(libraryBranch.Name, libraryBranch.Id);

            if (!isNameUnique)
                throw new InvalidOperationException("A library branch with the same name already exists.");

            existingBranch.Name = libraryBranch.Name;
            existingBranch.Address = libraryBranch.Address;
            existingBranch.City = libraryBranch.City;
            existingBranch.PhoneNumber = libraryBranch.PhoneNumber;

            await _libraryBranchRepository.UpdateAsync(existingBranch);
        }

        // Deletes a library branch by its identifier
        public async Task DeleteAsync(int id)
        {
            var libraryBranch = await _libraryBranchRepository.GetByIdAsync(id);

            if (libraryBranch is null)
                throw new InvalidOperationException("Library branch not found.");

            await _libraryBranchRepository.DeleteAsync(libraryBranch);
        }
    }
}
