namespace LibraryManagement.API.DTOs.Request
{
    public class CategoryRequestDTO
    {
        // Category basic information
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
