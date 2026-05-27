namespace LibraryManagement.API.DTOs.Request
{
    public class BookAuthorRequestDTO
    {
        // Book associated with the relationship
        public int BookId { get; set; }

        // Author associated with the relationship
        public int AuthorId { get; set; }
    }
}
