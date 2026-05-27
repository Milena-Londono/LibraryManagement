using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface IBookAuthorRepository : IGenericRepository<BookAuthor>
    {
        Task<IEnumerable<BookAuthor>> GetByBookIdAsync(int bookId);

        Task<IEnumerable<BookAuthor>> GetByAuthorIdAsync(int authorId);

        Task<bool> ExistsAsync(int bookId, int authorId);
    }
}
