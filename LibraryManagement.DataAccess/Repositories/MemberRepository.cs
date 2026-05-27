using LibraryManagement.DataAccess.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DataAccess.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(LibraryDbContext context) : base(context)
        {
        }

        // Retrieves a member by document number
        public async Task<Member?> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.DocumentNumber == documentNumber);
        }

        // Retrieves a member by email
        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.Email == email);
        }

        // Validates whether the document number is unique
        public async Task<bool> IsDocumentNumberUniqueAsync(
            string documentNumber,
            int? excludeMemberId = null)
        {
            return !await _context.Members
                .AnyAsync(m =>
                    m.DocumentNumber == documentNumber &&
                    (!excludeMemberId.HasValue || m.Id != excludeMemberId.Value));
        }

        // Validates whether the email is unique
        public async Task<bool> IsEmailUniqueAsync(
            string email,
            int? excludeMemberId = null)
        {
            return !await _context.Members
                .AnyAsync(m =>
                    m.Email == email &&
                    (!excludeMemberId.HasValue || m.Id != excludeMemberId.Value));
        }
    }
}
