using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Response
{
    public class UserResponseDTO
    {
        // User identifier
        public int Id { get; set; }

        // User basic information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // User role and status
        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
