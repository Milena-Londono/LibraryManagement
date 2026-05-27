using LibraryManagement.Domain.Enums;

namespace LibraryManagement.API.DTOs.Response
{
    public class MemberResponseDTO
    {
        // Member identifier
        public int Id { get; set; }

        // Member basic information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string DocumentNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        // Membership information
        public DateTime RegistrationDate { get; set; }

        public MemberStatus Status { get; set; }

        // Audit information
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
