using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Response
{
    public class BookResponseDTO
    {
        // Book identifier
        public int Id { get; set; }

        // Book basic information
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime PublishedDate { get; set; }

        // Copies information
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        // Book status
        public BookStatus Status { get; set; }

        // Related information
        public string CategoryName { get; set; } = string.Empty;

        public string LibraryBranchName { get; set; } = string.Empty;

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
