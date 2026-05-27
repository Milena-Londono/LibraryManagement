namespace LibraryManagement.API.DTOs.Response
{
    public class BookAuthorResponseDTO
    {
        // Relationship identifier
        public int Id { get; set; }

        // Related book information
        public int BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        // Related author information
        public int AuthorId { get; set; }

        public string AuthorFullName { get; set; } = string.Empty;

        // Relationship creation date
        public DateTime AssignedAt { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
