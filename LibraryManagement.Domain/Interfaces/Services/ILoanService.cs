using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface ILoanService
    {
        // Retrieves all loans
        Task<IEnumerable<Loan>> GetAllAsync();

        // Retrieves a loan by its identifier
        Task<Loan?> GetByIdAsync(int id);

        // Retrieves all active loans
        Task<IEnumerable<Loan>> GetActiveLoansAsync();

        // Retrieves the loan history for a member
        Task<IEnumerable<Loan>> GetLoansByMemberAsync(int memberId);

        // Retrieves overdue loans
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();

        // Creates a loan after validating book availability and member status
        Task<Loan> CreateAsync(Loan loan);

        // Marks a loan as returned and updates book availability
        Task ReturnBookAsync(int loanId);

        // Cancels an active loan
        Task CancelAsync(int loanId);
    }
}
