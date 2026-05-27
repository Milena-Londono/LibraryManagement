namespace LibraryManagement.API.DTOs.Response
{
    public class AuthorResponseDTO
    {
        // Author identifier
        public int Id { get; set; }

        // Author basic information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Nationality { get; set; }

        public DateTime? BirthDate { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
