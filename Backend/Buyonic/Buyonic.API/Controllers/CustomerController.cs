using Buyonic.BLL;
using Buyonic.BLL.Buyonic.BLL;
using Buyonic.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    //[Authorize]
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

        //[Authorize(Roles ="Admin")]
        [HttpPost("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool deleted = await _customerManager.DeleteCustomerAsync(id);
            if (deleted)
                return Ok("Customer Successfully Deleted");
            return BadRequest($"Error during deleting the customer with ID {id}");
        }

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
