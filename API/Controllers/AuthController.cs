using Domain.Dtos.IdentityDtos;
using Domain.Services.Auth;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<User> userManager, IAuthService authService)
        {
            _authService = authService;
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto reg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterAsync(reg);
            if (!result.IsAuthenticated)
                return BadRequest(new { message = result.Message });

            return Ok(result);

        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.LoginAsync(login);
            if (!result.IsAuthenticated)
                return BadRequest(new { message = result.Message });
            return Ok(result);
        }
    }
}
