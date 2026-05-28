using AutoMapper;
using LibraryManagement.API.DTOs.Request;
using LibraryManagement.API.DTOs.Response;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        // GET: api/Member
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _memberService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MemberResponseDTO>>(members));
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberService.GetByIdAsync(id);

            if (member is null)
                return NotFound("Member not found.");

            return Ok(_mapper.Map<MemberResponseDTO>(member));
        }

        // GET: api/Member/document/12345
        [HttpGet("document/{documentNumber}")]
        public async Task<IActionResult> GetByDocumentNumber(string documentNumber)
        {
            var member = await _memberService.GetByDocumentNumberAsync(documentNumber);

            if (member is null)
                return NotFound("Member not found.");

            return Ok(_mapper.Map<MemberResponseDTO>(member));
        }

        // GET: api/Member/email/test@email.com
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var member = await _memberService.GetByEmailAsync(email);

            if (member is null)
                return NotFound("Member not found.");

            return Ok(_mapper.Map<MemberResponseDTO>(member));
        }

        // POST: api/Member
        [HttpPost]
        public async Task<IActionResult> Create(MemberRequestDTO request)
        {
            try
            {
                var member = _mapper.Map<Member>(request);
                var createdMember = await _memberService.CreateAsync(member);
                var response = _mapper.Map<MemberResponseDTO>(createdMember);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Member/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MemberRequestDTO request)
        {
            try
            {
                var member = _mapper.Map<Member>(request);
                member.Id = id;

                await _memberService.UpdateAsync(member);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/Member/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _memberService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // Converts business validation messages into HTTP responses
        private IActionResult HandleBusinessException(InvalidOperationException ex)
        {
            if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                return NotFound(ex.Message);

            if (ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                return Conflict(ex.Message);

            return BadRequest(ex.Message);
        }
    }
}
