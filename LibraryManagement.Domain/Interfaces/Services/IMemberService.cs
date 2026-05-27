using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface IMemberService
    {
        // Retrieves all members
        Task<IEnumerable<Member>> GetAllAsync();

        // Retrieves a member by its identifier
        Task<Member?> GetByIdAsync(int id);

        // Retrieves a member by document number
        Task<Member?> GetByDocumentNumberAsync(string documentNumber);

        // Retrieves a member by email
        Task<Member?> GetByEmailAsync(string email);

        // Creates a new member after business validations
        Task<Member> CreateAsync(Member member);

        // Updates an existing member after business validations
        Task UpdateAsync(Member member);

        // Deletes a member by its identifier
        Task DeleteAsync(int id);
    }
}
