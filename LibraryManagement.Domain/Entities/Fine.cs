using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Fine : AuditBase
    {
        // Foreign Key
        public int LoanId { get; set; }

        // Fine Information
        public decimal Amount { get; set; }

        public string Reason { get; set; } = string.Empty;

        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

        public DateTime? PaidDate { get; set; }

        public FineStatus Status { get; set; } = FineStatus.Pending;

        // Navigation Property
        public Loan Loan { get; set; } = null!;
    }
}
