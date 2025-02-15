using System.Threading.Tasks;
using Identity.API.Core.Models;

namespace Identity.API.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterModel model, string origin);
        Task<AuthResult> LoginAsync(LoginModel model);
        Task<AuthResult> SignInAsync(LoginModel model);
        Task SignOutAsync();
        Task<AuthResult> ConfirmEmailAsync(string userId, string token);
        Task<AuthResult> ForgotPasswordAsync(string email, string origin);
        Task<AuthResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}
