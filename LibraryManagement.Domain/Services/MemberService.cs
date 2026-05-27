using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<MemberService> _logger;

        public MemberService(
            IMemberRepository memberRepository,
            ILogger<MemberService> logger)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }

        // Retrieves all registered members
        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        // Retrieves a member by its identifier
        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        // Retrieves a member by document number
        public async Task<Member?> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _memberRepository.GetByDocumentNumberAsync(documentNumber);
        }

        // Retrieves a member by email
        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _memberRepository.GetByEmailAsync(email);
        }

        // Creates a member after validating required fields and duplicates
        public async Task<Member> CreateAsync(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.FirstName))
                throw new InvalidOperationException("Member first name is required.");

            if (string.IsNullOrWhiteSpace(member.LastName))
                throw new InvalidOperationException("Member last name is required.");

            if (string.IsNullOrWhiteSpace(member.DocumentNumber))
                throw new InvalidOperationException("Document number is required.");

            if (string.IsNullOrWhiteSpace(member.Email))
                throw new InvalidOperationException("Email is required.");

            var isDocumentUnique = await _memberRepository
                .IsDocumentNumberUniqueAsync(member.DocumentNumber);

            if (!isDocumentUnique)
                throw new InvalidOperationException("A member with the same document number already exists.");

            var isEmailUnique = await _memberRepository
                .IsEmailUniqueAsync(member.Email);

            if (!isEmailUnique)
                throw new InvalidOperationException("A member with the same email already exists.");

            _logger.LogInformation(
                "Creating member with document number {DocumentNumber}",
                member.DocumentNumber);

            return await _memberRepository.AddAsync(member);
        }

        // Updates a member after validating existence and duplicates
        public async Task UpdateAsync(Member member)
        {
            var existingMember = await _memberRepository.GetByIdAsync(member.Id);

            if (existingMember is null)
                throw new InvalidOperationException("Member not found.");

            var isDocumentUnique = await _memberRepository
                .IsDocumentNumberUniqueAsync(member.DocumentNumber, member.Id);

            if (!isDocumentUnique)
                throw new InvalidOperationException("A member with the same document number already exists.");

            var isEmailUnique = await _memberRepository
                .IsEmailUniqueAsync(member.Email, member.Id);

            if (!isEmailUnique)
                throw new InvalidOperationException("A member with the same email already exists.");

            existingMember.FirstName = member.FirstName;
            existingMember.LastName = member.LastName;
            existingMember.DocumentNumber = member.DocumentNumber;
            existingMember.Email = member.Email;
            existingMember.PhoneNumber = member.PhoneNumber;
            existingMember.Status = member.Status;

            await _memberRepository.UpdateAsync(existingMember);
        }

        // Deletes a member by its identifier
        public async Task DeleteAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member is null)
                throw new InvalidOperationException("Member not found.");

            await _memberRepository.DeleteAsync(member);
        }
    }
}
