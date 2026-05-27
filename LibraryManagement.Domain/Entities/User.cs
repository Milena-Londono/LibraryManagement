using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class User : AuditBase
    {
        // Basic Information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Authentication Information
        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Member;

        public bool IsActive { get; set; } = true;
    }
}
