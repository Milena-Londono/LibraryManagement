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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(
                _mapper.Map<IEnumerable<UserResponseDTO>>(users));
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
                return NotFound("User not found.");

            return Ok(
                _mapper.Map<UserResponseDTO>(user));
        }

        // GET: api/User/email/admin@library.com
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user is null)
                return NotFound("User not found.");

            return Ok(
                _mapper.Map<UserResponseDTO>(user));
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create(UserRequestDTO request)
        {
            try
            {
                var user = _mapper.Map<User>(request);

                var createdUser =
                    await _userService.CreateAsync(user);

                var response =
                    _mapper.Map<UserResponseDTO>(createdUser);

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

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UserRequestDTO request)
        {
            try
            {
                var user = _mapper.Map<User>(request);

                user.Id = id;

                await _userService.UpdateAsync(user);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return HandleBusinessException(ex);
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);

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
