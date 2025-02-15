using Identity.API.Core.Interfaces;
using Identity.API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var origin = $"{Request.Scheme}://{Request.Host.Value}";
            var result = await _authService.RegisterAsync(model, origin);

            if (result.Successful)
            {
                return Ok("Registration successful. Please check your email to confirm your account.");
            }

            return BadRequest(result.Errors);
        }
   

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
            var result = await _authService.SignInAsync(model);

            if (result.Successful)
            {
                return Ok(new { Token = result.Token });
            }

            return Unauthorized(result.Errors);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _authService.SignOutAsync();
            return Ok("Signed out successfully");
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _authService.ConfirmEmailAsync(userId, token);

            if (result.Successful)
            {
                return Ok("Email confirmed successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var origin = $"{Request.Scheme}://{Request.Host.Value}";
            var result = await _authService.ForgotPasswordAsync(model.Email, origin);

            if (result.Successful)
            {
                return Ok("Password reset email sent");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var result = await _authService.ResetPasswordAsync(model);

            if (result.Successful)
            {
                return Ok("Password has been reset successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}
