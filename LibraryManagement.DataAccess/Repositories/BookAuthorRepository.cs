using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves all author relationships for a specific book
        public async Task<IEnumerable<BookAuthor>> GetByBookIdAsync(int bookId)
        {
            return await _context.BookAuthors
                .Include(ba => ba.Author)
                .Include(ba => ba.Book)
                .Where(ba => ba.BookId == bookId)
                .ToListAsync();
        }

        // Retrieves all book relationships for a specific author
        public async Task<IEnumerable<BookAuthor>> GetByAuthorIdAsync(int authorId)
        {
            return await _context.BookAuthors
                .Include(ba => ba.Author)
                .Include(ba => ba.Book)
                .Where(ba => ba.AuthorId == authorId)
                .ToListAsync();
        }

        // Validates whether the relationship already exists
        public async Task<bool> ExistsAsync(int bookId, int authorId)
        {
            return await _context.BookAuthors
                .AnyAsync(ba =>
                    ba.BookId == bookId &&
                    ba.AuthorId == authorId);
        }
    }
}
