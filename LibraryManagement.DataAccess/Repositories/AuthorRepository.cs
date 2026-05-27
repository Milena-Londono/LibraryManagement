using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    // Repository responsible for author data access operations.
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }

        /// Searches authors by first name or last name.
        public async Task<IEnumerable<Author>> SearchByNameAsync(string name)
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name))
                .ToListAsync();
        }

        /// Validates whether an author with the same full name already exists.
        public async Task<bool> ExistsByFullNameAsync(
            string firstName,
            string lastName,
            int? excludeAuthorId = null)
        {
            return await _context.Authors
                .AnyAsync(a =>
                    a.FirstName == firstName &&
                    a.LastName == lastName &&
                    (!excludeAuthorId.HasValue || a.Id != excludeAuthorId.Value));
        }
    }
}
