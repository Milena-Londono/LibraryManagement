using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> SearchByNameAsync(string name);

        Task<bool> ExistsByFullNameAsync(string firstName, string lastName, int? excludeAuthorId = null);
    }
}
