namespace LibraryManagement.Domain.Entities
{
    public class LibraryBranch : AuditBase
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
