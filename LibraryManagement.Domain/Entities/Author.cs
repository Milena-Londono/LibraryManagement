namespace LibraryManagement.Domain.Entities
{
    public class Author : AuditBase
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Nationality { get; set; }

        public DateTime? BirthDate { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
