using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces.Services
{
    public interface IFineService
    {
        // Retrieves all fines
        Task<IEnumerable<Fine>> GetAllAsync();

        // Retrieves a fine by its identifier
        Task<Fine?> GetByIdAsync(int id);

        // Retrieves all pending fines
        Task<IEnumerable<Fine>> GetPendingFinesAsync();

        // Retrieves all fines associated with a member
        Task<IEnumerable<Fine>> GetFinesByMemberAsync(int memberId);

        // Creates a fine after business validations
        Task<Fine> CreateAsync(Fine fine);

        // Marks a fine as paid
        Task MarkAsPaidAsync(int fineId);

        // Cancels a fine
        Task CancelAsync(int fineId);
    }
}
