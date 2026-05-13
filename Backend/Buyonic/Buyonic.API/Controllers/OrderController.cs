using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        // GET api/order/customer/{customerId}
        [Authorize(Roles ="Admin,Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            var orders = await _orderManager.GetOrdersByCustomerIdAsync(customerId);
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
