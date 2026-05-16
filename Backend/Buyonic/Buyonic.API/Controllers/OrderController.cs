using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly ISellerManager _sellerManager;

        public OrderController(IOrderManager orderManager, ISellerManager sellerManager)
        {
            _orderManager = orderManager;
            _sellerManager = sellerManager;
        }

        // GET api/order/customer/{customerId}
        [Authorize(Roles ="Admin,Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            var orders = await _orderManager.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        // GET api/order/seller/{sellerId}
        [Authorize(Roles = "Seller,Admin")]
        [HttpGet("seller/{sellerId:int}")]
        public async Task<IActionResult> GetOrdersBySeller(int sellerId)
        {
            if (User.IsInRole("Seller") && !User.IsInRole("Admin"))
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                    return Unauthorized();

                var seller = await _sellerManager.GetSellerByEmailAsync(email);
                if (seller == null || seller.Id != sellerId)
                    return Forbid();
            }

            var orders = await _orderManager.GetOrdersBySellerIdAsync(sellerId);
            return Ok(orders);
        }

        // GET api/order/all  (before {orderId})
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderManager.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET api/order/{orderId}
        [Authorize(Roles ="Admin,Customer")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _orderManager.GetOrderWithItemsAsync(orderId);
            if (order == null)
                return NotFound("Order not found.");

            return Ok(order);
        }

        // POST api/order
        [Authorize(Roles ="Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderManager.CreateOrderAsync(dto);
            return Ok(order);
        }

        // PUT api/order/{orderId}/status
        [Authorize(Roles ="Seller,Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            var order = await _orderManager.UpdateOrderStatusAsync(orderId, status);
            if (order == null)
                return NotFound("Order not found.");

            return Ok(order);
        }
    }
}
