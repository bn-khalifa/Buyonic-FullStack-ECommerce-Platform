using Buyonic.BLL.DTOs.Payment;
using Buyonic.BLL.Managers.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;

        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        // GET api/payment/methods
        [HttpGet("methods")]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var methods = await _paymentManager.GetAllPaymentMethodsAsync();
            return Ok(methods);
        }

        // GET api/payment/methods/{id}
        [HttpGet("methods/{id}")]
        public async Task<IActionResult> GetPaymentMethod(int id)
        {
            var method = await _paymentManager.GetPaymentMethodByIdAsync(id);
            if (method == null)
                return NotFound("Payment method not found.");

            return Ok(method);
        }

        // GET api/payment/customer/{customerId}
        [Authorize(Roles ="Admin,Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerPayments(int customerId)
        {
            var payments = await _paymentManager.GetCustomerPaymentsAsync(customerId);
            return Ok(payments);
        }

        // POST api/payment/add
        [Authorize(Roles ="Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddCustomerPayment(AddCustomerPaymentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = await _paymentManager.AddCustomerPaymentAsync(dto);
            return Ok(payment);
        }

        // DELETE api/payment/{customerPaymentId}
        [Authorize(Roles ="Admin")]
        [HttpDelete("{customerPaymentId}")]
        public async Task<IActionResult> RemoveCustomerPayment(int customerPaymentId)
        {
            var result = await _paymentManager.RemoveCustomerPaymentAsync(customerPaymentId);
            if (!result)
                return NotFound("Customer payment not found.");

            return Ok("Payment method removed successfully.");
        }
    }
}
