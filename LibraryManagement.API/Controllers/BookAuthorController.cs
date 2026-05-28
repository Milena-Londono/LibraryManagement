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
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;
        private readonly IMapper _mapper;

        public BookAuthorController(
            IBookAuthorService bookAuthorService,
            IMapper mapper)
        {
            _bookAuthorService = bookAuthorService;
            _mapper = mapper;
        }

        // GET: api/BookAuthor
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var relationships =
                await _bookAuthorService.GetAllAsync();

            return Ok(
                _mapper.Map<IEnumerable<BookAuthorResponseDTO>>(relationships));
        }

        // GET: api/BookAuthor/book/5
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(int bookId)
        {
            var relationships =
                await _bookAuthorService.GetByBookIdAsync(bookId);

            return Ok(
                _mapper.Map<IEnumerable<BookAuthorResponseDTO>>(relationships));
        }

        // GET: api/BookAuthor/author/3
        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthorId(int authorId)
        {
            var relationships =
                await _bookAuthorService.GetByAuthorIdAsync(authorId);

            return Ok(
                _mapper.Map<IEnumerable<BookAuthorResponseDTO>>(relationships));
        }

        // POST: api/BookAuthor
        [HttpPost]
        public async Task<IActionResult> AssignAuthorToBook(
            BookAuthorRequestDTO request)
        {
            try
            {
                var relationship =
                    _mapper.Map<BookAuthor>(request);

                var createdRelationship =
                    await _bookAuthorService
                        .AssignAuthorToBookAsync(relationship);

                var response =
                    _mapper.Map<BookAuthorResponseDTO>(
                        createdRelationship);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/BookAuthor/book/5/author/3
        [HttpDelete("book/{bookId}/author/{authorId}")]
        public async Task<IActionResult> RemoveAuthorFromBook(
            int bookId,
            int authorId)
        {
            try
            {
                await _bookAuthorService
                    .RemoveAuthorFromBookAsync(bookId, authorId);

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
                    StringComparison.OrdinalIgnoreCase))
            {
                return Conflict(ex.Message);
            }

            return BadRequest(ex.Message);
        }
    }
}
