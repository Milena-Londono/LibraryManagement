using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface IAuthorService
    {
        // Retrieves all authors
        Task<IEnumerable<Author>> GetAllAsync();

        // Retrieves an author by its identifier
        Task<Author?> GetByIdAsync(int id);

        // Searches authors by first name or last name
        Task<IEnumerable<Author>> SearchByNameAsync(string name);

        // Creates a new author after business validations
        Task<Author> CreateAsync(Author author);

        // Updates an existing author after business validations
        Task UpdateAsync(Author author);

        // Deletes an author by its identifier
        Task DeleteAsync(int id);
    }
}
