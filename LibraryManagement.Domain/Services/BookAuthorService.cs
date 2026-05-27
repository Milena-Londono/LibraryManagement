using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<BookAuthorService> _logger;

        public BookAuthorService(
            IBookAuthorRepository bookAuthorRepository,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ILogger<BookAuthorService> logger)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _logger = logger;
        }

        // Retrieves all book-author relationships
        public async Task<IEnumerable<BookAuthor>> GetAllAsync()
        {
            return await _bookAuthorRepository.GetAllAsync();
        }

        // Retrieves all authors assigned to a specific book
        public async Task<IEnumerable<BookAuthor>> GetByBookIdAsync(int bookId)
        {
            return await _bookAuthorRepository.GetByBookIdAsync(bookId);
        }

        // Retrieves all books assigned to a specific author
        public async Task<IEnumerable<BookAuthor>> GetByAuthorIdAsync(int authorId)
        {
            return await _bookAuthorRepository.GetByAuthorIdAsync(authorId);
        }

        // Assigns an author to a book after validating existence and duplicates
        public async Task<BookAuthor> AssignAuthorToBookAsync(BookAuthor bookAuthor)
        {
            var bookExists = await _bookRepository.ExistsAsync(bookAuthor.BookId);

            if (!bookExists)
                throw new InvalidOperationException("Book not found.");

            var authorExists = await _authorRepository.ExistsAsync(bookAuthor.AuthorId);

            if (!authorExists)
                throw new InvalidOperationException("Author not found.");

            var relationshipExists = await _bookAuthorRepository
                .ExistsAsync(bookAuthor.BookId, bookAuthor.AuthorId);

            if (relationshipExists)
                throw new InvalidOperationException("This author is already assigned to the book.");

            bookAuthor.AssignedAt = DateTime.UtcNow;

            _logger.LogInformation(
                "Assigning author {AuthorId} to book {BookId}",
                bookAuthor.AuthorId,
                bookAuthor.BookId);

            return await _bookAuthorRepository.AddAsync(bookAuthor);
        }

        // Removes an author from a book
        public async Task RemoveAuthorFromBookAsync(int bookId, int authorId)
        {
            var relationships = await _bookAuthorRepository.GetByBookIdAsync(bookId);

            var relationship = relationships
                .FirstOrDefault(ba => ba.AuthorId == authorId);

            if (relationship is null)
                throw new InvalidOperationException("Book-author relationship not found.");

            await _bookAuthorRepository.DeleteAsync(relationship);
        }
    }
}
