namespace LibraryManagement.API.DTOs.Response
{
    public class LibraryBranchResponseDTO
    {
        // Library branch identifier
        public int Id { get; set; }

        // Library branch basic information
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
