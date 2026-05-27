using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILibraryBranchRepository _libraryBranchRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(
            IBookRepository bookRepository,
            ICategoryRepository categoryRepository,
            ILibraryBranchRepository libraryBranchRepository,
            ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _libraryBranchRepository = libraryBranchRepository;
            _logger = logger;
        }

        // Retrieves all registered books
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        // Retrieves a book by its identifier
        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        // Retrieves books that belong to a specific category
        public async Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId)
        {
            return await _bookRepository.GetByCategoryAsync(categoryId);
        }

        // Retrieves books with available copies
        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _bookRepository.GetAvailableBooksAsync();
        }

        // Creates a book after validating ISBN, category and branch
        public async Task<Book> CreateAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new InvalidOperationException("Book title is required.");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new InvalidOperationException("ISBN is required.");

            if (book.TotalCopies <= 0)
                throw new InvalidOperationException("Total copies must be greater than zero.");

            if (book.AvailableCopies < 0 || book.AvailableCopies > book.TotalCopies)
                throw new InvalidOperationException("Available copies must be between zero and total copies.");

            var isIsbnUnique = await _bookRepository.IsIsbnUniqueAsync(book.ISBN);
            if (!isIsbnUnique)
                throw new InvalidOperationException("A book with the same ISBN already exists.");

            var categoryExists = await _categoryRepository.ExistsAsync(book.CategoryId);
            if (!categoryExists)
                throw new InvalidOperationException("Category does not exist.");

            var branchExists = await _libraryBranchRepository.ExistsAsync(book.LibraryBranchId);
            if (!branchExists)
                throw new InvalidOperationException("Library branch does not exist.");

            _logger.LogInformation("Creating book with ISBN {ISBN}", book.ISBN);

            return await _bookRepository.AddAsync(book);
        }

        // Updates a book after validating existence and duplicated ISBN
        public async Task UpdateAsync(Book book)
        {
            var existingBook = await _bookRepository.GetByIdAsync(book.Id);
            if (existingBook is null)
                throw new InvalidOperationException("Book not found.");

            var isIsbnUnique = await _bookRepository.IsIsbnUniqueAsync(book.ISBN, book.Id);
            if (!isIsbnUnique)
                throw new InvalidOperationException("A book with the same ISBN already exists.");

            existingBook.Title = book.Title;
            existingBook.ISBN = book.ISBN;
            existingBook.Description = book.Description;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.AvailableCopies = book.AvailableCopies;
            existingBook.Status = book.Status;
            existingBook.CategoryId = book.CategoryId;
            existingBook.LibraryBranchId = book.LibraryBranchId;

            await _bookRepository.UpdateAsync(existingBook);
        }

        // Deletes a book by its identifier
        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
                throw new InvalidOperationException("Book not found.");

            await _bookRepository.DeleteAsync(book);
        }
    }
}
