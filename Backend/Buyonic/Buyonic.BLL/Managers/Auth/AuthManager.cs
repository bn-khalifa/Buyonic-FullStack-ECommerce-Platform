using Buyonic.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Buyonic.BLL
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public AuthManager(UserManager<ApplicationUser> userManager, 
                            IConfiguration configuration, 
                            IUnitOfWork unitOfWork, 
                            IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<IEnumerable<IdentityError>?> RegisterAsync(RegisterDTO dto)
        {
            var user = new ApplicationUser
            {
                firstName = dto.FirstName,
                lastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                isActive = true,
                createdAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return result.Errors;

            var accountType = char.ToUpper(dto.AccountType[0]) + dto.AccountType.Substring(1).ToLower();
            await _userManager.AddToRoleAsync(user, accountType);

            if (accountType == "Customer")
                _unitOfWork.CustomerRepository.Add(new Customer { userId = user.Id, address = dto.Address });
            else if (accountType == "Seller")
                _unitOfWork.SellerRepository.Add(new Seller { userId = user.Id, storeName = dto.StoreName });

            await _unitOfWork.SaveAsync();
            return null;
        }

        public async Task<string?> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return null;

            var isCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isCorrect) return null;

            return await GenerateTokenAsync(user);
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.firstName),
                new Claim(ClaimTypes.Surname, user.lastName),
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ForgotPasswordAsync(string email, string resetBaseUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var resetLink = $"{resetBaseUrl}?email={email}&token={encodedToken}";

            var body = $@"
        <h3>Reset Your Password</h3>
        <p>Click the link below to reset your password. This link expires in 1 hour.</p>
        <a href='{resetLink}'>Reset Password</a>
    ";

            await _emailService.SendAsync(email, "Reset Your Buyonic Password", body);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            var decodedToken = Uri.UnescapeDataString(dto.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);
            return result.Succeeded;
        }
    }
}