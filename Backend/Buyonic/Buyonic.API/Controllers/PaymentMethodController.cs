using Buyonic.DAL.Repositories.PaymentMethodRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {

        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodController(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        // GET: api/PaymentMethod
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var methods = await _paymentMethodRepository.GetAllPaymentMethodsAsync();

            if (methods == null || !methods.Any())
                return NotFound("No payment methods found");

            return Ok(methods);
        }

        // GET: api/PaymentMethod/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var method = await _paymentMethodRepository.GetPaymentMethodByIdAsync(id);

            if (method == null)
                return NotFound("Payment method not found");

            return Ok(method);
        }
    }
}

