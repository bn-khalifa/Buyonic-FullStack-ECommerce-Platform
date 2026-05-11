using Buyonic.DAL.Repositories.CartRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // Get cart by customer id
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCartByCustomerId(int customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);

            if (cart == null)
                return NotFound("Cart not found for this customer");

            return Ok(cart);
        }

        // Get cart with items
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartWithItems(int cartId)
        {
            var cart = await _cartRepository.GetCartWithItemsAsync(cartId);

            if (cart == null)
                return NotFound("Cart not found");

            return Ok(cart);
        }

        // Get specific cart item
        [HttpGet("{cartId}/items/{productId}")]
        public async Task<IActionResult> GetCartItem(int cartId, int productId)
        {
            var item = await _cartRepository.GetCartItemAsync(cartId, productId);

            if (item == null)
                return NotFound("Cart item not found");

            return Ok(item);
        }
    }
}
