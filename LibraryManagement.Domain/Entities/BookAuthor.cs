namespace LibraryManagement.Domain.Entities
{
    public class BookAuthor : AuditBase
    {
        // Foreign Keys
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        // Additional Information
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Book Book { get; set; } = null!;

        public Author Author { get; set; } = null!;
    }
}
