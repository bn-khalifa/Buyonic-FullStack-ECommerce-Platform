using Buyonic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.ProductRepository
                .GetAllAsync();

            return Ok(products);
        }

        // GET: api/product/with-sellers
        [HttpGet("with-sellers")]
        public async Task<IActionResult> GetAllWithSellers()
        {
            var products = await _unitOfWork.ProductRepository
                .GetAllProductsWithSellersAsync();

            return Ok(products);
        }

        // GET: api/product/with-categories
        [HttpGet("with-categories")]
        public async Task<IActionResult> GetAllWithCategories()
        {
            var products = await _unitOfWork.ProductRepository
                .GetAllProductsWithCategoriesAsync();

            return Ok(products);
        }

        // GET: api/product/with-order-items
        [HttpGet("with-order-items")]
        public async Task<IActionResult> GetAllWithOrderItems()
        {
            var products = await _unitOfWork.ProductRepository
                .GetAllProductsWithOrderItemsAsync();

            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/product/by-category/3
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _unitOfWork.ProductRepository
                .GetProductsByCategoryAsync(categoryId);

            return Ok(products);
        }

        // GET: api/product/by-seller/2
        [HttpGet("by-seller/{sellerId}")]
        public async Task<IActionResult> GetBySeller(int sellerId)
        {
            var products = await _unitOfWork.ProductRepository
                .GetProductsBySellerAsync(sellerId);

            return Ok(products);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _unitOfWork.ProductRepository.Add(product);

            await _unitOfWork.SaveAsync();

            return Ok(product);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetByIdAsync(id);

            if (product == null)
                return NotFound();

            _unitOfWork.ProductRepository.Delete(product);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}