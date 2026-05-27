using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Request
{
    public class MemberRequestDTO
    {
        // Member basic information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string DocumentNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        // Member status
        public MemberStatus Status { get; set; } = MemberStatus.Active;
    }
}
