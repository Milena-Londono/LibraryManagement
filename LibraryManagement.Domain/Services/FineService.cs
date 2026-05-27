using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _fineRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<FineService> _logger;

        public FineService(
            IFineRepository fineRepository,
            ILoanRepository loanRepository,
            ILogger<FineService> logger)
        {
            _fineRepository = fineRepository;
            _loanRepository = loanRepository;
            _logger = logger;
        }

        // Retrieves all registered fines
        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            return await _fineRepository.GetAllAsync();
        }

        // Retrieves a fine by its identifier
        public async Task<Fine?> GetByIdAsync(int id)
        {
            return await _fineRepository.GetByIdAsync(id);
        }

        // Retrieves all pending fines
        public async Task<IEnumerable<Fine>> GetPendingFinesAsync()
        {
            return await _fineRepository.GetPendingFinesAsync();
        }

        // Retrieves all fines associated with a member
        public async Task<IEnumerable<Fine>> GetFinesByMemberAsync(int memberId)
        {
            return await _fineRepository.GetFinesByMemberAsync(memberId);
        }

        // Creates a fine after validating the associated loan
        public async Task<Fine> CreateAsync(Fine fine)
        {
            var loan = await _loanRepository.GetByIdAsync(fine.LoanId);

            if (loan is null)
                throw new InvalidOperationException("Loan not found.");

            var existingFine = await _fineRepository.GetByLoanIdAsync(fine.LoanId);

            if (existingFine is not null)
                throw new InvalidOperationException("This loan already has a fine.");

            if (fine.Amount <= 0)
                throw new InvalidOperationException("Fine amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(fine.Reason))
                throw new InvalidOperationException("Fine reason is required.");

            fine.IssuedDate = DateTime.UtcNow;
            fine.Status = FineStatus.Pending;

            _logger.LogInformation(
                "Creating fine for loan {LoanId}",
                fine.LoanId);

            return await _fineRepository.AddAsync(fine);
        }

        // Marks a fine as paid
        public async Task MarkAsPaidAsync(int fineId)
        {
            var fine = await _fineRepository.GetByIdAsync(fineId);

            if (fine is null)
                throw new InvalidOperationException("Fine not found.");

            if (fine.Status == FineStatus.Paid)
                throw new InvalidOperationException("Fine is already paid.");

            fine.Status = FineStatus.Paid;
            fine.PaidDate = DateTime.UtcNow;

            await _fineRepository.UpdateAsync(fine);
        }

        // Cancels a fine
        public async Task CancelAsync(int fineId)
        {
            var fine = await _fineRepository.GetByIdAsync(fineId);

            if (fine is null)
                throw new InvalidOperationException("Fine not found.");

            if (fine.Status == FineStatus.Paid)
                throw new InvalidOperationException("Paid fines cannot be cancelled.");

            fine.Status = FineStatus.Cancelled;

            await _fineRepository.UpdateAsync(fine);
        }
    }
}
