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
    public class LibraryBranchController : ControllerBase
    {
        private readonly ILibraryBranchService _libraryBranchService;
        private readonly IMapper _mapper;

        public LibraryBranchController(
            ILibraryBranchService libraryBranchService,
            IMapper mapper)
        {
            _libraryBranchService = libraryBranchService;
            _mapper = mapper;
        }

        // GET: api/LibraryBranch
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _libraryBranchService.GetAllAsync();

            return Ok(
                _mapper.Map<IEnumerable<LibraryBranchResponseDTO>>(branches));
        }

        // GET: api/LibraryBranch/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _libraryBranchService.GetByIdAsync(id);

            if (branch is null)
                return NotFound("Library branch not found.");

            return Ok(
                _mapper.Map<LibraryBranchResponseDTO>(branch));
        }

        // GET: api/LibraryBranch/city/Bogota
        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetByCity(string city)
        {
            var branches = await _libraryBranchService.GetByCityAsync(city);

            return Ok(
                _mapper.Map<IEnumerable<LibraryBranchResponseDTO>>(branches));
        }

        // POST: api/LibraryBranch
        [HttpPost]
        public async Task<IActionResult> Create(LibraryBranchRequestDTO request)
        {
            try
            {
                var branch = _mapper.Map<LibraryBranch>(request);

                var createdBranch =
                    await _libraryBranchService.CreateAsync(branch);

                var response =
                    _mapper.Map<LibraryBranchResponseDTO>(createdBranch);

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

        // PUT: api/LibraryBranch/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            LibraryBranchRequestDTO request)
        {
            try
            {
                var branch = _mapper.Map<LibraryBranch>(request);

                branch.Id = id;

                await _libraryBranchService.UpdateAsync(branch);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/LibraryBranch/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _libraryBranchService.DeleteAsync(id);

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
                    "already exists",
                    StringComparison.OrdinalIgnoreCase))
            {
                return Conflict(ex.Message);
            }

            return BadRequest(ex.Message);
        }
    }
}
