using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize]
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

        // GET: api/seller/me  (must be before {id} so "me" is not bound as an int)
        [Authorize(Roles = "Seller")]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentSeller()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return Unauthorized();

            var seller = await _sellerManager.GetSellerByEmailAsync(email);
            if (seller == null)
            {
                return NotFound(new
                {
                    message = "Seller profile not found for this account."
                });
            }

            return Ok(seller);
        }

        // GET: api/seller/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller = await _sellerManager.GetSellerByIdAsync(id);

            if (seller == null)
            {
                return NotFound(new
                {
                    message = "Seller not found"
                });
            }

            return Ok(seller);
        }

        // GET: api/seller/5/with-products
        [HttpGet("{id}/with-products")]
        public async Task<IActionResult> GetByIdWithProducts(int id)
        {
            var seller = await _sellerManager.GetSellerByIdWithProductsAsync(id);

            if (seller == null)
            {
                return NotFound(new
                {
                    message = "Seller not found"
                });
            }

            return Ok(seller);
        }

        // GET: api/seller/by-email?email=test@test.com
        [Authorize(Roles = "Admin")]
        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var seller = await _sellerManager.GetSellerByEmailAsync(email);

            if (seller == null)
            {
                return NotFound(new
                {
                    message = "Seller not found"
                });
            }

            return Ok(seller);
        }

        // POST: api/seller
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSellerDTO seller)
        {
            var createdSeller = await _sellerManager.AddSellerAsync(seller);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdSeller.Id },
                createdSeller
            );
        }

        // PUT: api/seller/5
        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSellerDTO seller)
        {
            if (User.IsInRole("Seller") && !User.IsInRole("Admin"))
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                    return Unauthorized();

                var me = await _sellerManager.GetSellerByEmailAsync(email);
                if (me == null || me.Id != id)
                    return Forbid();

                seller = new UpdateSellerDTO { StoreName = seller.StoreName };
            }

            var updated = await _sellerManager.UpdateSellerAsync(id, seller);

            if (!updated)
            {
                return NotFound(new
                {
                    message = "Seller not found"
                });
            }

            return NoContent();
        }

        // DELETE: api/seller/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _sellerManager.DeleteSellerAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Seller not found"
                });
            }

            return NoContent();
        }
    }
}