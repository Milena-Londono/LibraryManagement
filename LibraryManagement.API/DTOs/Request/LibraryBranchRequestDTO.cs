namespace LibraryManagement.API.DTOs.Request
{

    public class LibraryBranchRequestDTO
    {
        // Library branch basic information
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
    }
}
