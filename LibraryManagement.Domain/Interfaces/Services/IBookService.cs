using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface IBookService
    {
        // Retrieves all books
        Task<IEnumerable<Book>> GetAllAsync();

        // Retrieves a book by its identifier
        Task<Book?> GetByIdAsync(int id);

        // Retrieves books by category
        Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId);

        // Retrieves only books with available copies
        Task<IEnumerable<Book>> GetAvailableBooksAsync();

        // Creates a new book after business validations
        Task<Book> CreateAsync(Book book);

        // Updates an existing book after business validations
        Task UpdateAsync(Book book);

        // Deletes a book by its identifier
        Task DeleteAsync(int id);
    }
}
