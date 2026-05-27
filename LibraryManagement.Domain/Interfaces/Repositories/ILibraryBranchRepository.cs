using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface ILibraryBranchRepository : IGenericRepository<LibraryBranch>
    {
        Task<LibraryBranch?> GetByNameAsync(string name);

        Task<IEnumerable<LibraryBranch>> GetByCityAsync(string city);

        Task<bool> IsNameUniqueAsync(string name, int? excludeLibraryBranchId = null);
    }
}
