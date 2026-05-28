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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(
                _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories));
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
                return NotFound("Category not found.");

            return Ok(
                _mapper.Map<CategoryResponseDTO>(category));
        }

        // GET: api/Category/name/Fiction
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _categoryService.GetByNameAsync(name);

            if (category is null)
                return NotFound("Category not found.");

            return Ok(
                _mapper.Map<CategoryResponseDTO>(category));
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestDTO request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);

                var createdCategory =
                    await _categoryService.CreateAsync(category);

                var response =
                    _mapper.Map<CategoryResponseDTO>(createdCategory);

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

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CategoryRequestDTO request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);

                category.Id = id;

                await _categoryService.UpdateAsync(category);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);

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
