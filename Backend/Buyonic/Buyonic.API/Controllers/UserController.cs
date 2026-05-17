using Buyonic.BLL;
using Buyonic.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly ISellerManager _sellerManager;
        private readonly IAuthManager _authManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            ICustomerManager customerManager,
            ISellerManager sellerManager,
            IAuthManager authManager,
            UserManager<ApplicationUser> userManager)
        {
            _customerManager = customerManager;
            _sellerManager = sellerManager;
            _authManager = authManager;
            _userManager = userManager;
        }

        private async Task<UserLookupResultDTO> BuildLookupAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var accountType = roles.FirstOrDefault() ?? string.Empty;

            var dto = new UserLookupResultDTO
            {
                AccountType = accountType,
                ApplicationUserId = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.firstName,
                LastName = user.lastName,
                IsActive = user.isActive,
                IsDeleted = user.isDeleted
            };

            dto.Customer = await _customerManager.GetCustomerByUserIdAsync(user.Id);
            dto.Seller = await _sellerManager.GetSellerByUserIdAsync(user.Id);
            return dto;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("lookup")]
        public async Task<IActionResult> LookupUser([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Query (q) is required.");

            q = q.Trim();

            if (q.Contains('@', StringComparison.Ordinal))
            {
                var user = await _userManager.FindByEmailAsync(q);
                if (user == null)
                    return NotFound("No user with that email.");

                return Ok(await BuildLookupAsync(user));
            }

            if (!int.TryParse(q, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id))
                return BadRequest("Enter a valid email or numeric id.");

            var customer = await _customerManager.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                var user = await _userManager.FindByIdAsync(customer.UserId.ToString());
                if (user == null) return NotFound();
                return Ok(await BuildLookupAsync(user));
            }

            var seller = await _sellerManager.GetSellerByIdAsync(id);
            if (seller != null)
            {
                var user = await _userManager.FindByIdAsync(seller.UserId.ToString());
                if (user == null) return NotFound();
                return Ok(await BuildLookupAsync(user));
            }

            var byUser = await _userManager.FindByIdAsync(id.ToString());
            if (byUser == null)
                return NotFound("No user, customer, or seller with that id.");

            return Ok(await BuildLookupAsync(byUser));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{userId:int}/active")]
        public async Task<IActionResult> SetUserActive(int userId, [FromBody] SetUserActiveDTO dto)
        {
            if (dto == null)
                return BadRequest("Body is required.");

            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentId)
                && currentId == userId
                && !dto.IsActive)
            {
                return BadRequest("You cannot deactivate your own account.");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return NotFound("User not found.");

            user.isActive = dto.IsActive;
            if (dto.IsActive)
                user.isDeleted = false;
            user.updatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }

        // Add Admin
        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.AccountType = "Admin";
            var errors = await _authManager.RegisterAsync(dto);
            if (errors != null)
                return BadRequest(errors);

            return Ok("Admin created successfully.");
        }

        // Seller Related Actions
        [Authorize(Roles = "Admin")]
        [HttpGet("seller")]
        public async Task<ActionResult> GetAllSellers([FromQuery] bool includeProducts)
        {
            if (includeProducts)
            {
                var sellers = await _sellerManager.GetSellersWithProductsAsync();
                return Ok(sellers);
            }
            else
            {
                var sellers = await _sellerManager.GetSellersAsync();
                return Ok(sellers);
            }
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("seller/{id:int}")]
        public async Task<ActionResult<SellerDTO>> GetSellerById([FromRoute] int id, [FromQuery] bool includeProducts)
        {
            if (includeProducts)
            {
                var seller = await _sellerManager.GetSellerByIdWithProductsAsync(id);
                if (seller == null) return NotFound();
                return Ok(seller);
            }
            else
            {
                var seller = await _sellerManager.GetSellerByIdAsync(id);
                if (seller == null) return NotFound();
                return Ok(seller);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("seller/search")]
        public async Task<ActionResult> GetSellerByEmail([FromQuery] string email, [FromQuery] bool includeProducts)
        {
            if (includeProducts)
            {
                var result = await _sellerManager.GetSellerByEmailWithProductsAsync(email);
                if (result == null) return NotFound();
                return Ok(result);
            }
            else
            {
                var result = await _sellerManager.GetSellerByEmailAsync(email);
                if (result == null) return NotFound();
                return Ok(result);
            }
        }
    }
}
