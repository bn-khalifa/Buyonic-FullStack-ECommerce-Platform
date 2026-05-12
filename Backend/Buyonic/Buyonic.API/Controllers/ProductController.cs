using Buyonic.BLL.Managers.ProductMng;
using Buyonic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productManager.GetProductsAsync();
            return Ok(products);
        }

        // GET: api/product/with-sellers
        [HttpGet("with-sellers")]
        public async Task<IActionResult> GetAllWithSellers()
        {
            var products = await _productManager.GetProductsWithSellersAsync();
            return Ok(products);
        }

        // GET: api/product/with-categories
        [HttpGet("with-categories")]
        public async Task<IActionResult> GetAllWithCategories()
        {
            var products = await _productManager.GetProductsWithCategoriesAsync();
            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productManager.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/product/by-category/3
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productManager.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        // GET: api/product/by-seller/2
        [HttpGet("by-seller/{sellerId}")]
        public async Task<IActionResult> GetBySeller(int sellerId)
        {
            var products = await _productManager.GetProductsBySellerAsync(sellerId);
            return Ok(products);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productManager.AddProductAsync(product);
            return Ok(product);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productManager.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productManager.DeleteProductAsync(id);
            return NoContent();
        }
    }
}