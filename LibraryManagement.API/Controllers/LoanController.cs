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
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;

        public LoanController(ILoanService loanService, IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        // GET: api/Loan
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<LoanResponseDTO>>(loans));
        }

        // GET: api/Loan/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if (loan is null)
                return NotFound("Loan not found.");

            return Ok(_mapper.Map<LoanResponseDTO>(loan));
        }

        // GET: api/Loan/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLoans()
        {
            var loans = await _loanService.GetActiveLoansAsync();
            return Ok(_mapper.Map<IEnumerable<LoanResponseDTO>>(loans));
        }

        // GET: api/Loan/member/5
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetLoansByMember(int memberId)
        {
            var loans = await _loanService.GetLoansByMemberAsync(memberId);
            return Ok(_mapper.Map<IEnumerable<LoanResponseDTO>>(loans));
        }

        // GET: api/Loan/overdue
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueLoans()
        {
            var loans = await _loanService.GetOverdueLoansAsync();
            return Ok(_mapper.Map<IEnumerable<LoanResponseDTO>>(loans));
        }

        // POST: api/Loan
        [HttpPost]
        public async Task<IActionResult> Create(LoanRequestDTO request)
        {
            try
            {
                var loan = _mapper.Map<Loan>(request);
                var createdLoan = await _loanService.CreateAsync(loan);
                var response = _mapper.Map<LoanResponseDTO>(createdLoan);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Loan/5/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            try
            {
                await _loanService.ReturnBookAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Loan/5/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _loanService.CancelAsync(id);
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

            if (ex.Message.Contains("already", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("no available", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("Only active", StringComparison.OrdinalIgnoreCase))
                return Conflict(ex.Message);

            return BadRequest(ex.Message);
        }
    }
}
