using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);

        Task<bool> IsNameUniqueAsync(string name, int? excludeCategoryId = null);
    }
}
