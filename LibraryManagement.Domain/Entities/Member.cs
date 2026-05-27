using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities
{
    public class Member : AuditBase
    {
        // Basic Information
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string DocumentNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        // Membership Information
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public MemberStatus Status { get; set; } = MemberStatus.Active;

        // Navigation Properties
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
