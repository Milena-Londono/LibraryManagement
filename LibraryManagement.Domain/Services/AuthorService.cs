using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(
            IAuthorRepository authorRepository,
            ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        // Retrieves all registered authors
        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        // Retrieves an author by its identifier
        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        // Searches authors by first name or last name
        public async Task<IEnumerable<Author>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Search name is required.");

            return await _authorRepository.SearchByNameAsync(name);
        }

        // Creates an author after validating required fields and duplicates
        public async Task<Author> CreateAsync(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.FirstName))
                throw new InvalidOperationException("Author first name is required.");

            if (string.IsNullOrWhiteSpace(author.LastName))
                throw new InvalidOperationException("Author last name is required.");

            var exists = await _authorRepository.ExistsByFullNameAsync(
                author.FirstName,
                author.LastName);

            if (exists)
                throw new InvalidOperationException("An author with the same full name already exists.");

            _logger.LogInformation("Creating author {FirstName} {LastName}", author.FirstName, author.LastName);

            return await _authorRepository.AddAsync(author);
        }

        // Updates an author after validating existence and duplicates
        public async Task UpdateAsync(Author author)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(author.Id);
            if (existingAuthor is null)
                throw new InvalidOperationException("Author not found.");

            var exists = await _authorRepository.ExistsByFullNameAsync(
                author.FirstName,
                author.LastName,
                author.Id);

            if (exists)
                throw new InvalidOperationException("An author with the same full name already exists.");

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.Nationality = author.Nationality;
            existingAuthor.BirthDate = author.BirthDate;

            await _authorRepository.UpdateAsync(existingAuthor);
        }

        // Deletes an author by its identifier
        public async Task DeleteAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author is null)
                throw new InvalidOperationException("Author not found.");

            await _authorRepository.DeleteAsync(author);
        }
    }
}
