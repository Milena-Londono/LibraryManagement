using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves a category by name
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        // Validates whether the category name already exists
        public async Task<bool> IsNameUniqueAsync(
            string name,
            int? excludeCategoryId = null)
        {
            return !await _context.Categories
                .AnyAsync(c =>
                    c.Name == name &&
                    (!excludeCategoryId.HasValue || c.Id != excludeCategoryId.Value));
        }
    }
}
