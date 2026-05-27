using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<LoanService> _logger;

        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            ILogger<LoanService> logger)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }

        // Retrieves all registered loans
        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        // Retrieves a loan by its identifier
        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _loanRepository.GetByIdAsync(id);
        }

        // Retrieves all active loans
        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            return await _loanRepository.GetActiveLoansAsync();
        }

        // Retrieves the loan history for a member
        public async Task<IEnumerable<Loan>> GetLoansByMemberAsync(int memberId)
        {
            return await _loanRepository.GetLoansByMemberAsync(memberId);
        }

        // Retrieves overdue loans
        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            return await _loanRepository.GetOverdueLoansAsync();
        }

        // Creates a loan after validating book availability and member status
        public async Task<Loan> CreateAsync(Loan loan)
        {
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book is null)
                throw new InvalidOperationException("Book not found.");

            var member = await _memberRepository.GetByIdAsync(loan.MemberId);
            if (member is null)
                throw new InvalidOperationException("Member not found.");

            if (member.Status != MemberStatus.Active)
                throw new InvalidOperationException("Only active members can request loans.");

            if (book.AvailableCopies <= 0)
                throw new InvalidOperationException("Book has no available copies.");

            var activeLoan = await _loanRepository.GetActiveLoanByBookAsync(loan.BookId);
            if (activeLoan is not null)
                throw new InvalidOperationException("This book already has an active loan.");

            loan.LoanDate = DateTime.UtcNow;
            loan.Status = LoanStatus.Active;

            book.AvailableCopies--;

            if (book.AvailableCopies == 0)
                book.Status = BookStatus.Loaned;

            await _bookRepository.UpdateAsync(book);

            _logger.LogInformation(
                "Creating loan for book {BookId} and member {MemberId}",
                loan.BookId,
                loan.MemberId);

            return await _loanRepository.AddAsync(loan);
        }

        // Marks a loan as returned and updates book availability
        public async Task ReturnBookAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan is null)
                throw new InvalidOperationException("Loan not found.");

            if (loan.Status != LoanStatus.Active && loan.Status != LoanStatus.Overdue)
                throw new InvalidOperationException("Only active or overdue loans can be returned.");

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book is null)
                throw new InvalidOperationException("Book not found.");

            loan.ReturnDate = DateTime.UtcNow;
            loan.Status = LoanStatus.Returned;

            book.AvailableCopies++;

            if (book.AvailableCopies > 0)
                book.Status = BookStatus.Available;

            await _bookRepository.UpdateAsync(book);
            await _loanRepository.UpdateAsync(loan);
        }

        // Cancels an active loan and restores book availability
        public async Task CancelAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan is null)
                throw new InvalidOperationException("Loan not found.");

            if (loan.Status != LoanStatus.Active)
                throw new InvalidOperationException("Only active loans can be cancelled.");

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book is null)
                throw new InvalidOperationException("Book not found.");

            loan.Status = LoanStatus.Cancelled;

            book.AvailableCopies++;

            if (book.AvailableCopies > 0)
                book.Status = BookStatus.Available;

            await _bookRepository.UpdateAsync(book);
            await _loanRepository.UpdateAsync(loan);
        }
    }
}
