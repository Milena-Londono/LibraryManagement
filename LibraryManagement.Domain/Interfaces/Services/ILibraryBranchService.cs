using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface ILibraryBranchService
    {
        // Retrieves all library branches
        Task<IEnumerable<LibraryBranch>> GetAllAsync();

        // Retrieves a library branch by its identifier
        Task<LibraryBranch?> GetByIdAsync(int id);

        // Retrieves library branches by city
        Task<IEnumerable<LibraryBranch>> GetByCityAsync(string city);

        // Creates a new library branch after business validations
        Task<LibraryBranch> CreateAsync(LibraryBranch libraryBranch);

        // Updates an existing library branch after business validations
        Task UpdateAsync(LibraryBranch libraryBranch);

        // Deletes a library branch by its identifier
        Task DeleteAsync(int id);
    }
}
