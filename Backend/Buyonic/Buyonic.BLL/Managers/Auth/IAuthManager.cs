using Microsoft.AspNetCore.Identity;

namespace Buyonic.BLL
{
    public interface IAuthManager
    {
        Task<string?> LoginAsync(LoginDTO dto);
        Task<IEnumerable<IdentityError>?> RegisterAsync(RegisterDTO dto);
        Task<bool> ForgotPasswordAsync(string email, string resetBaseUrl);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO dto);
    }
}