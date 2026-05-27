using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class FineRepository : GenericRepository<Fine>, IFineRepository
    {
        public FineRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves the fine associated with a specific loan
        public async Task<Fine?> GetByLoanIdAsync(int loanId)
        {
            return await _context.Fines
                .Include(f => f.Loan)
                .FirstOrDefaultAsync(f => f.LoanId == loanId);
        }

        // Retrieves all pending fines
        public async Task<IEnumerable<Fine>> GetPendingFinesAsync()
        {
            return await _context.Fines
                .Include(f => f.Loan)
                .ThenInclude(l => l.Member)
                .Where(f => f.Status == FineStatus.Pending)
                .ToListAsync();
        }

        // Retrieves all fines associated with a specific member
        public async Task<IEnumerable<Fine>> GetFinesByMemberAsync(int memberId)
        {
            return await _context.Fines
                .Include(f => f.Loan)
                .ThenInclude(l => l.Member)
                .Where(f => f.Loan.MemberId == memberId)
                .ToListAsync();
        }
    }
}
