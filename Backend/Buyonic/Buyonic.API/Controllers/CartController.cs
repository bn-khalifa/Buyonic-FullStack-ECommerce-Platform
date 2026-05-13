using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Authorize(Roles ="Customer")]
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var cart = await _cartManager.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
                return NotFound("Cart not found for this customer.");

            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int customerId, int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            await _cartManager.AddToCartAsync(customerId, productId, quantity);
            return Ok("Product added to cart successfully.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem(int cartId, int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            await _cartManager.UpdateCartItemAsync(cartId, productId, quantity);
            return Ok("Cart item updated successfully.");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
        {
            await _cartManager.RemoveFromCartAsync(cartId, productId);
            return Ok("Product removed from cart successfully.");
        }

        [HttpDelete("clear/{cartId}")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            await _cartManager.ClearCartAsync(cartId);
            return Ok("Cart cleared successfully.");
        }
    }
}


//    public class CartController : ControllerBase
//    {
//        private readonly ICartRepository _cartRepository;

//        public CartController(ICartRepository cartRepository)
//        {
//            _cartRepository = cartRepository;
//        }

//        // Get cart by customer id
//        [HttpGet("customer/{customerId}")]
//        public async Task<IActionResult> GetCartByCustomerId(int customerId)
//        {
//            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);

//            if (cart == null)
//                return NotFound("Cart not found for this customer");

//            return Ok(cart);
//        }

//        // Get cart with items
//        [HttpGet("{cartId}")]
//        public async Task<IActionResult> GetCartWithItems(int cartId)
//        {
//            var cart = await _cartRepository.GetCartWithItemsAsync(cartId);

//            if (cart == null)
//                return NotFound("Cart not found");

//            return Ok(cart);
//        }

//        // Get specific cart item
//        [HttpGet("{cartId}/items/{productId}")]
//        public async Task<IActionResult> GetCartItem(int cartId, int productId)
//        {
//            var item = await _cartRepository.GetCartItemAsync(cartId, productId);

//            if (item == null)
//                return NotFound("Cart item not found");

//            return Ok(item);
//        }
//    }
//}
