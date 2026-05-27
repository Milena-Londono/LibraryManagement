using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves all active loans
        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.Status == LoanStatus.Active)
                .ToListAsync();
        }

        // Retrieves all loans for a specific member
        public async Task<IEnumerable<Loan>> GetLoansByMemberAsync(int memberId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.MemberId == memberId)
                .ToListAsync();
        }

        // Retrieves all loans for a specific book
        public async Task<IEnumerable<Loan>> GetLoansByBookAsync(int bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.BookId == bookId)
                .ToListAsync();
        }

        // Retrieves overdue loans
        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l =>
                    l.DueDate < DateTime.UtcNow &&
                    l.Status == LoanStatus.Active)
                .ToListAsync();
        }

        // Retrieves the active loan for a specific book
        public async Task<Loan?> GetActiveLoanByBookAsync(int bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l =>
                    l.BookId == bookId &&
                    l.Status == LoanStatus.Active);
        }
    }
}
