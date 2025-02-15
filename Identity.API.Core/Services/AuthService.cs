using Identity.API.Core.Interfaces;
using Identity.API.Core.Models;
using Identity.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = new[] { "Invalid credentials" }
                };
            }

            var token = GenerateJwtToken(user);
            return new AuthResult
            {
                Successful = true,
                Token = token
            };
        }

        public async Task<AuthResult> SignInAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = new[] { "Invalid login attempt" }
                };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = GenerateJwtToken(user);

            return new AuthResult
            {
                Successful = true,
                Token = token
            };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSecret = _configuration["Jwt:Secret"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var key = Encoding.UTF8.GetBytes(jwtSecret);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model, string origin)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{origin}/api/account/confirmemail?userId={user.Id}&token={token}";

            // Logic to send email with the confirmation link can be added here

            return new AuthResult
            {
                Successful = true
            };
        }

        public async Task<AuthResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new AuthResult
            {
                Successful = true
            };
        }

        public async Task<AuthResult> ForgotPasswordAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Logic to send email with the token can be added here

            return new AuthResult
            {
                Successful = true
            };
        }

        public async Task<AuthResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Successful = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new AuthResult
            {
                Successful = true
            };
        }
    }
}
