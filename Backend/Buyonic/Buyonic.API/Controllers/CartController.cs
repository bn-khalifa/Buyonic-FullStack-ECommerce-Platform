using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        //[HttpGet("{customerId}")]
        //public async Task<IActionResult> GetCart(int customerId)
        //{
        //    var cart = await _cartManager.GetCartByCustomerIdAsync(customerId);
        //    if (cart == null)
        //        return NotFound("Cart not found for this customer.");
        //    return Ok(cart);
        //}
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var cart = await _cartManager.GetCartByEmailAsync(email);
            if (cart == null)
                return NotFound("Cart not found for this customer.");

            return Ok(cart);
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> AddToCart(int customerId, int productId, int quantity)
        //{
        //    if (quantity <= 0)
        //        return BadRequest("Quantity must be greater than zero.");
        //    await _cartManager.AddToCartAsync(customerId, productId, quantity);
        //    return Ok("Product added to cart successfully.");
        //}
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            await _cartManager.AddToCartAsync(email, productId, quantity);
            return Ok("Product added to cart successfully.");
        }

        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateCartItem(int cartId, int productId, int quantity)
        //{
        //    if (quantity <= 0)
        //        return BadRequest("Quantity must be greater than zero.");
        //    await _cartManager.UpdateCartItemAsync(cartId, productId, quantity);
        //    return Ok("Cart item updated successfully.");
        //}
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem(int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            await _cartManager.UpdateCartItemAsync(email, productId, quantity);
            return Ok("Cart item updated successfully.");
        }

        //[HttpDelete("remove")]
        //public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
        //{
        //    await _cartManager.RemoveFromCartAsync(cartId, productId);
        //    return Ok("Product removed from cart successfully.");
        //}
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            await _cartManager.RemoveFromCartAsync(email, productId);
            return Ok("Product removed from cart successfully.");
        }

        //[HttpDelete("clear/{cartId}")]
        //public async Task<IActionResult> ClearCart(int cartId)
        //{
        //    await _cartManager.ClearCartAsync(cartId);
        //    return Ok("Cart cleared successfully.");
        //}
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            await _cartManager.ClearCartAsync(email);
            return Ok("Cart cleared successfully.");
        }
    }
}