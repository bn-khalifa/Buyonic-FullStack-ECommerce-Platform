using Buyonic.BLL;
using Buyonic.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(ICustomerManager customerManager, IUnitOfWork unitOfWork)
        {
            _customerManager = customerManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers([FromQuery] bool includeOrders)
        {
            if (includeOrders)
            {
                var customers = await _customerManager.GetAllCustomersWithOrdersAsync();
                return Ok(customers);
            }
            else
            {
                var customers = await _customerManager.GetCustomersAsync();
                return Ok(customers);
            }
        }

        // GET: api/customer/me  (before {id} so "me" is not parsed as an int)
        [Authorize(Roles = "Customer")]
        [HttpGet("me")]
        public async Task<ActionResult<CustomerDTO>> GetCurrentCustomer()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var customer = await _customerManager.GetCustomerByEmailAsync(email);
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById([FromRoute] int id)
        {
            var customer = await _customerManager.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerByEmail([FromQuery] string email)
        {
            var customer = await _customerManager.GetCustomerByEmailAsync(email);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool deleted = await _customerManager.DeleteCustomerAsync(id);
            if (deleted)
                return Ok("Customer Successfully Deleted");
            return BadRequest($"Error during deleting the customer with ID {id}");
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPut("update")]
        public async Task<ActionResult> Update(CustomerDTO customer)
        {
            try
            {
                await _customerManager.UpdateCustomerAsync(customer);
                return Ok("Customer Updated Successfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
