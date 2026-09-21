using Microsoft.AspNetCore.Mvc;
using SmartKnowledgeHub.API.Services;
using SmartKnowledgeHub.API.DTOs;

namespace SmartKnowledgeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result)
            {
                return BadRequest("User with the same email already exists.");
            }
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.LoginAsync(dto);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok("Login Successful.");
        }

    }
}
