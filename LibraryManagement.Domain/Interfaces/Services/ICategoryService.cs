using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        // Retrieves all categories
        Task<IEnumerable<Category>> GetAllAsync();

        // Retrieves a category by its identifier
        Task<Category?> GetByIdAsync(int id);

        // Retrieves a category by name
        Task<Category?> GetByNameAsync(string name);

        // Creates a new category after business validations
        Task<Category> CreateAsync(Category category);

        // Updates an existing category after business validations
        Task UpdateAsync(Category category);

        // Deletes a category by its identifier
        Task DeleteAsync(int id);
    }
}
