namespace LibraryManagement.API.DTOs.Request
{
    public class LoanRequestDTO
    {
        // Book associated with the loan
        public int BookId { get; set; }

        // Member requesting the loan
        public int MemberId { get; set; }

        // Expected return date
        public DateTime DueDate { get; set; }
    }
}
