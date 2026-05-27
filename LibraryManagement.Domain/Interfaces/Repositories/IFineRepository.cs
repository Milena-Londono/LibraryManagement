using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface IFineRepository : IGenericRepository<Fine>
    {
        Task<Fine?> GetByLoanIdAsync(int loanId);

        Task<IEnumerable<Fine>> GetPendingFinesAsync();

        Task<IEnumerable<Fine>> GetFinesByMemberAsync(int memberId);
    }
}
