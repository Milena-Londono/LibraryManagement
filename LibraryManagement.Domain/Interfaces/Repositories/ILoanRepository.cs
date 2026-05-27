using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetActiveLoansAsync();

        Task<IEnumerable<Loan>> GetLoansByMemberAsync(int memberId);

        Task<IEnumerable<Loan>> GetLoansByBookAsync(int bookId);

        Task<IEnumerable<Loan>> GetOverdueLoansAsync();

        Task<Loan?> GetActiveLoanByBookAsync(int bookId);
    }
}
