using Buyonic.BLL.Managers.SellerMng;
using Buyonic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerManager _sellerManager;

        public SellerController(ISellerManager sellerManager)
        {
            _sellerManager = sellerManager;
        }

        // GET: api/seller
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sellers = await _sellerManager.GetSellersAsync();
            return Ok(sellers);
        }

        // GET: api/seller/with-products
        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllWithProducts()
        {
            var sellers = await _sellerManager.GetSellersWithProductsAsync();
            return Ok(sellers);
        }

        // GET: api/seller/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller = await _sellerManager.GetSellerByIdAsync(id);

            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        // GET: api/seller/5/with-products
        [HttpGet("{id}/with-products")]
        public async Task<IActionResult> GetByIdWithProducts(int id)
        {
            var seller = await _sellerManager.GetSellerByIdWithProductsAsync(id);

            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        // GET: api/seller/by-email/test@test.com
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var seller = await _sellerManager.GetSellerByEmailAsync(email);

            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        // POST: api/seller
        [HttpPost]
        public async Task<IActionResult> Create(Seller seller)
        {
            await _sellerManager.AddSellerAsync(seller);
            return Ok(seller);
        }

        // PUT: api/seller/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Seller seller)
        {
            if (id != seller.Id)
                return BadRequest();

            await _sellerManager.UpdateSellerAsync(seller);
            return NoContent();
        }

        // DELETE: api/seller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerManager.DeleteSellerAsync(id);
            return NoContent();
        }
    }
}