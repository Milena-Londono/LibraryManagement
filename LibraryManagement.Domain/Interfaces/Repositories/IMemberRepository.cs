using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member?> GetByDocumentNumberAsync(string documentNumber);

        Task<Member?> GetByEmailAsync(string email);

        Task<bool> IsDocumentNumberUniqueAsync(string documentNumber, int? excludeMemberId = null);

        Task<bool> IsEmailUniqueAsync(string email, int? excludeMemberId = null);
    }
}
