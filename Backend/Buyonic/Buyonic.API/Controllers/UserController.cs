using Buyonic.BLL;
using Buyonic.BLL.Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    //[Authorize(Roles = "Admin")]
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

        //[HttpGet("{id:int}/products")]
        //public async Task<ActionResult<SellerWithProductsDTO>> GetSellerWithProducts([FromRoute] int id)
        //{
        //    var seller = await _sellerManager.GetSellerByIdWithProductsAsync(id);
        //    if (seller == null) return NotFound();
        //    return Ok(seller);
        //}
    }
}