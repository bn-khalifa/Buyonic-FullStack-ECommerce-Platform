using Buyonic.BLL;
using Buyonic.BLL.Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly ISellerManager _sellerManager;

        public UserController(ICustomerManager customerManager, ISellerManager sellerManager)
        {
            _customerManager = customerManager;
            _sellerManager = sellerManager;
        }

        // Customer Related Actions
        [HttpGet("customer")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerManager.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("customer/{id:int}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById([FromRoute] int id)
        {
            var customer = await _customerManager.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("customer/search")]
        public async Task<ActionResult<SellerDTO>> GetCustomererByEmail([FromQuery] string email)
        {
            var customer = await _customerManager.GetCustomerByEmailAsync(email);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // Seller Related Actions
        [HttpGet("seller")]
        public async Task<ActionResult<IEnumerable<SellerDTO>>> GetAllSellers()
        {
            var sellers = await _sellerManager.GetSellersAsync();
            return Ok(sellers);
        }

        [HttpGet("seller/{id:int}")]
        public async Task<ActionResult<SellerDTO>> GetSellerById([FromRoute] int id)
        {
            var seller = await _sellerManager.GetSellerByIdAsync(id);
            if (seller == null) return NotFound();
            return Ok(seller);
        }

        [HttpGet("seller/search")]
        public async Task<ActionResult<SellerDTO>> GetSellerByEmail([FromQuery] string email)
        {
            var seller = await _sellerManager.GetSellerByEmailAsync(email);
            if (seller == null) return NotFound();
            return Ok(seller);
        }

        [HttpGet("{id:int}/products")]
        public async Task<ActionResult<SellerWithProductsDTO>> GetSellerWithProducts([FromRoute] int id)
        {
            var seller = await _sellerManager.GetSellerWithProductsAsync(id);
            if (seller == null) return NotFound();
            return Ok(seller);
        }
    }
}