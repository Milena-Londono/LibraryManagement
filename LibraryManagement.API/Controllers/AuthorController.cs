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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AuthorResponseDTO>>(authors));
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author is null)
                return NotFound("Author not found.");

            return Ok(_mapper.Map<AuthorResponseDTO>(author));
        }

        // GET: api/Author/search/name
        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                var authors = await _authorService.SearchByNameAsync(name);
                return Ok(_mapper.Map<IEnumerable<AuthorResponseDTO>>(authors));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> Create(AuthorRequestDTO request)
        {
            try
            {
                var author = _mapper.Map<Author>(request);
                var createdAuthor = await _authorService.CreateAsync(author);
                var response = _mapper.Map<AuthorResponseDTO>(createdAuthor);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorRequestDTO request)
        {
            try
            {
                var author = _mapper.Map<Author>(request);
                author.Id = id;

                await _authorService.UpdateAsync(author);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
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
