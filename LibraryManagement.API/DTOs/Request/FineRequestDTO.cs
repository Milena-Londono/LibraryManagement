namespace LibraryManagement.API.DTOs.Request
{
    public class FineRequestDTO
    {
        // Loan associated with the fine
        public int LoanId { get; set; }

        // Fine amount
        public decimal Amount { get; set; }

        // Reason for the fine
        public string Reason { get; set; } = string.Empty;
    }
}
