using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public UserController(ICustomerManager customerManager, ISellerManager sellerManager, IAuthManager authManager)
        {
            _customerManager = customerManager;
            _sellerManager = sellerManager;
            _authManager = authManager;
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