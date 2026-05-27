using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Request
{
    public class UserRequestDTO
    {
        // User basic information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // User password
        public string Password { get; set; } = string.Empty;

        // User role
        public UserRole Role { get; set; } = UserRole.Member;

        // Indicates whether the user is active
        public bool IsActive { get; set; } = true;
    }
}
