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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BookResponseDTO>>(books));
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDTO>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book is null)
                return NotFound("Book not found.");

            return Ok(_mapper.Map<BookResponseDTO>(book));
        }

        // GET: api/Book/category/1
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetByCategory(int categoryId)
        {
            var books = await _bookService.GetByCategoryAsync(categoryId);
            return Ok(_mapper.Map<IEnumerable<BookResponseDTO>>(books));
        }

        // GET: api/Book/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetAvailableBooks()
        {
            var books = await _bookService.GetAvailableBooksAsync();
            return Ok(_mapper.Map<IEnumerable<BookResponseDTO>>(books));
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> Create(BookRequestDTO request)
        {
            try
            {
                var book = _mapper.Map<Book>(request);
                var createdBook = await _bookService.CreateAsync(book);
                var response = _mapper.Map<BookResponseDTO>(createdBook);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookRequestDTO request)
        {
            try
            {
                var book = _mapper.Map<Book>(request);
                book.Id = id;

                await _bookService.UpdateAsync(book);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // Converts business validation messages into appropriate HTTP responses
        private IActionResult HandleBusinessException(InvalidOperationException ex)
        {
            if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
                return NotFound(ex.Message);

            if (ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                return Conflict(ex.Message);

            return BadRequest(ex.Message);
        }
    }
}
