using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Response
{
    public class FineResponseDTO
    {
        // Fine identifier
        public int Id { get; set; }

        // Fine information
        public decimal Amount { get; set; }

        public string Reason { get; set; } = string.Empty;

        public DateTime IssuedDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public FineStatus Status { get; set; }

        // Related information
        public int LoanId { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
