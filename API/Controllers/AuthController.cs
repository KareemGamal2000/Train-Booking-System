using Data.Models;
using Domain.Dtos.IdentityDtos;
using Domain.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ForgotPasswordAsync(model);
            return Ok(new { message = result });
        }


        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ResetPasswordAsync(model);

            // result is a string, so check for error keywords instead of .Succeeded
            if (result.Contains("فشل") || result.Contains("غير"))
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        // inside profile
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst("uid")?.Value
                     ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "المستخدم غير مصرح له" });

            var result = await _authService.ChangePasswordAsync(userId, model);

            // result is a string, so check for error keywords instead of .IsSuccess
            if (result.Contains("فشل") || result.Contains("غير"))
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        // inside profile
        [HttpPost("ChangeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst("uid")?.Value
                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _authService.ChangeEmailAsync(userId, model);

            if (result.Contains("فشل") || result.Contains("مستخدم"))
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        // inside profile
        [HttpPost("ConfirmEmailChange")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailChange([FromBody] ConfirmEmailChangeDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ConfirmEmailChangeAsync(model);

            if (result.Contains("فشل") || result.Contains("غير"))
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst("uid")?.Value
                                 ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "المستخدم غير مصرح له" });

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { message = "المستخدم غير موجود" });

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                userId = user.Id,
                userName = user.UserName,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                phoneNumber = user.PhoneNumber,
                role = roles.FirstOrDefault(),
                isActive = user.IsActive,
                lastLogin = user.LastLoginDate
            });
        }
    }
}
