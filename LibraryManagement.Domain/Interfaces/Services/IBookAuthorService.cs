using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface IBookAuthorService
    {
        // Retrieves all book-author relationships
        Task<IEnumerable<BookAuthor>> GetAllAsync();

        // Retrieves all authors assigned to a specific book
        Task<IEnumerable<BookAuthor>> GetByBookIdAsync(int bookId);

        // Retrieves all books assigned to a specific author
        Task<IEnumerable<BookAuthor>> GetByAuthorIdAsync(int authorId);

        // Assigns an author to a book after validating that the relationship does not already exist
        Task<BookAuthor> AssignAuthorToBookAsync(BookAuthor bookAuthor);

        // Removes an author from a book
        Task RemoveAuthorFromBookAsync(int bookId, int authorId);
    }
}
