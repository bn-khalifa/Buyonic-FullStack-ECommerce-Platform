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

        public AuthManager(UserManager<ApplicationUser> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
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
    }
}