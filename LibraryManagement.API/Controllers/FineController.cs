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
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;
        private readonly IMapper _mapper;

        public FineController(IFineService fineService, IMapper mapper)
        {
            _fineService = fineService;
            _mapper = mapper;
        }

        // GET: api/Fine
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fines = await _fineService.GetAllAsync();

            return Ok(
                _mapper.Map<IEnumerable<FineResponseDTO>>(fines));
        }

        // GET: api/Fine/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fine = await _fineService.GetByIdAsync(id);

            if (fine is null)
                return NotFound("Fine not found.");

            return Ok(
                _mapper.Map<FineResponseDTO>(fine));
        }

        // GET: api/Fine/pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingFines()
        {
            var fines = await _fineService.GetPendingFinesAsync();

            return Ok(
                _mapper.Map<IEnumerable<FineResponseDTO>>(fines));
        }

        // GET: api/Fine/member/5
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetFinesByMember(int memberId)
        {
            var fines = await _fineService.GetFinesByMemberAsync(memberId);

            return Ok(
                _mapper.Map<IEnumerable<FineResponseDTO>>(fines));
        }

        // POST: api/Fine
        [HttpPost]
        public async Task<IActionResult> Create(FineRequestDTO request)
        {
            try
            {
                var fine = _mapper.Map<Fine>(request);

                var createdFine =
                    await _fineService.CreateAsync(fine);

                var response =
                    _mapper.Map<FineResponseDTO>(createdFine);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = response.Id },
                    response);
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Fine/5/pay
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            try
            {
                await _fineService.MarkAsPaidAsync(id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Fine/5/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _fineService.CancelAsync(id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // Converts business validation messages into HTTP responses
        private IActionResult HandleBusinessException(
            InvalidOperationException ex)
        {
            if (ex.Message.Contains(
                    "not found",
                    StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(ex.Message);
            }

            if (ex.Message.Contains(
                    "already",
                    StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains(
                    "cannot",
                    StringComparison.OrdinalIgnoreCase))
            {
                return Conflict(ex.Message);
            }

            return BadRequest(ex.Message);
        }
    }
}
