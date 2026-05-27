using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Book : AuditBase
    {
        // Foreign Keys
        public int CategoryId { get; set; }

        public int LibraryBranchId { get; set; }

        // Basic Information
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime PublishedDate { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public BookStatus Status { get; set; } = BookStatus.Available;

        // Navigation Properties
        public Category Category { get; set; } = null!;

        public LibraryBranch LibraryBranch { get; set; } = null!;

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
