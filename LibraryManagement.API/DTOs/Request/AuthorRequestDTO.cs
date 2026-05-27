namespace LibraryManagement.API.DTOs.Request
{
    public class AuthorRequestDTO
    {
        // Author basic information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Nationality { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
