using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Loan : AuditBase
    {
        // Foreign Keys
        public int BookId { get; set; }

        public int MemberId { get; set; }

        // Loan Information
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; } = LoanStatus.Active;

        // Navigation Properties
        public Book Book { get; set; } = null!;

        public Member Member { get; set; } = null!;

        public Fine? Fine { get; set; }
    }
}
