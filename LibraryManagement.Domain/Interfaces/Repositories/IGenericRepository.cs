using LibraryManagement.Domain.Entities;
using System.Linq.Expressions;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : AuditBase
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistsAsync(int id);
    }
}
