using FintechWallet.Models;
using FintechWallet.Services;
using Microsoft.AspNetCore.Mvc;

namespace FintechWallet.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
            var response = await _authService.RegisterAsync(dto);
            if (response == null) return BadRequest("Username already exists.");
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response == null) return Unauthorized("Invalid username or password.");
            return Ok(response);
        }
    }
}
