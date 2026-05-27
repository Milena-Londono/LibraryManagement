namespace LibraryManagement.API.DTOs.Response
{
    public class CategoryResponseDTO
    {
        // Category identifier
        public int Id { get; set; }

        // Category basic information
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
