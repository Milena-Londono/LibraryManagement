using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class LibraryBranchRepository : GenericRepository<LibraryBranch>, ILibraryBranchRepository
    {
        public LibraryBranchRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves a library branch by name
        public async Task<LibraryBranch?> GetByNameAsync(string name)
        {
            return await _context.LibraryBranches
                .FirstOrDefaultAsync(lb => lb.Name == name);
        }

        // Retrieves library branches by city
        public async Task<IEnumerable<LibraryBranch>> GetByCityAsync(string city)
        {
            return await _context.LibraryBranches
                .Where(lb => lb.City == city)
                .ToListAsync();
        }

        // Validates whether the branch name already exists
        public async Task<bool> IsNameUniqueAsync(
            string name,
            int? excludeLibraryBranchId = null)
        {
            return !await _context.LibraryBranches
                .AnyAsync(lb =>
                    lb.Name == name &&
                    (!excludeLibraryBranchId.HasValue || lb.Id != excludeLibraryBranchId.Value));
        }
    }
}
