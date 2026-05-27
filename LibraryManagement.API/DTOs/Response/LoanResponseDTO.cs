using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Response
{
    public class LoanResponseDTO
    {
        // Loan identifier
        public int Id { get; set; }

        // Loan information
        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; }

        // Related information
        public int BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public int MemberId { get; set; }

        public string MemberFullName { get; set; } = string.Empty;

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
