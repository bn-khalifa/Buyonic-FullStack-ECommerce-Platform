using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistManager _wishlistManager;
        private readonly ICustomerManager _customerManager;

        public WishlistController(IWishlistManager wishlistManager, ICustomerManager customerManager)
        {
            _wishlistManager = wishlistManager;
            _customerManager = customerManager;
        }

        private async Task<int?> GetCustomerIdFromToken()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return null;
            var customer = await _customerManager.GetCustomerByEmailAsync(email);
            return customer?.Id;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var customerId = await GetCustomerIdFromToken();
            if (customerId == null) return Unauthorized();

            var wishlist = await _wishlistManager.GetWishlistAsync(customerId.Value);
            if (wishlist == null) return NotFound("Wishlist not found.");

            return Ok(wishlist);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWishlist([FromBody] AddToWishlistDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customerId = await GetCustomerIdFromToken();
            if (customerId == null) return Unauthorized();

            var result = await _wishlistManager.AddToWishlistAsync(customerId.Value, dto.ProductId);
            if (!result) return BadRequest("Product already in wishlist or not found.");

            return Ok("Product added to wishlist.");
        }

        [HttpDelete("remove/{productId:int}")]
        public async Task<IActionResult> RemoveFromWishlist([FromRoute] int productId)
        {
            var customerId = await GetCustomerIdFromToken();
            if (customerId == null) return Unauthorized();

            var result = await _wishlistManager.RemoveFromWishlistAsync(customerId.Value, productId);
            if (!result) return NotFound("Product not found in wishlist.");

            return Ok("Product removed from wishlist.");
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearWishlist()
        {
            var customerId = await GetCustomerIdFromToken();
            if (customerId == null) return Unauthorized();

            var result = await _wishlistManager.ClearWishlistAsync(customerId.Value);
            if (!result) return NotFound("Wishlist not found.");

            return Ok("Wishlist cleared.");
        }
    }
}