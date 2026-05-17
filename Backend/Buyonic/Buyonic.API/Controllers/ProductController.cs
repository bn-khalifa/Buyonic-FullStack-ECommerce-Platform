using Buyonic.BLL;
using Buyonic.BLL.Managers.ProductMng;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productManager.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("with-sellers")]
        public async Task<IActionResult> GetAllWithSellers()
        {
            var products = await _productManager.GetProductsWithSellersAsync();

            return Ok(products);
        }

        [HttpGet("with-categories")]
        public async Task<IActionResult> GetAllWithCategories()
        {
            var products = await _productManager.GetProductsWithCategoriesAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productManager.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product not found"
                });
            }

            return Ok(product);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productManager.GetProductsByCategoryAsync(categoryId);

            return Ok(products);
        }

        [HttpGet("by-seller/{sellerId}")]
        public async Task<IActionResult> GetBySeller(int sellerId)
        {
            var products = await _productManager.GetProductsBySellerAsync(sellerId);

            return Ok(products);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO product)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var createdProduct = await _productManager.AddProductAsync(product, email);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDTO product)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var isAdmin = User.IsInRole("Admin");
            var updated = await _productManager.UpdateProductAsync(id, product, email, isAdmin);

            if (!updated)
            {
                if (!isAdmin && email != null)
                {
                    var existing = await _productManager.GetProductByIdAsync(id);
                    if (existing != null)
                        return Forbid();
                }

                return NotFound(new { message = "Product not found" });
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productManager.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Product not found"
                });
            }

            return NoContent();
        }
    }
}