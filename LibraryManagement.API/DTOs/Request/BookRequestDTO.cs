using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Request
{
    public class BookRequestDTO
    {
        // Category assigned to the book
        public int CategoryId { get; set; }

        // Library branch where the book is located
        public int LibraryBranchId { get; set; }

        // Basic book information
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime PublishedDate { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public BookStatus Status { get; set; } = BookStatus.Available;
    }
}
