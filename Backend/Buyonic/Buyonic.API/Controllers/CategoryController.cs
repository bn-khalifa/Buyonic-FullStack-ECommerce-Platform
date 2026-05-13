using Buyonic.BLL;
using Buyonic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryManager.GetCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/category/with-products
        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllWithProducts()
        {
            var categories = await _categoryManager.GetCategoriesWithProductsAsync();
            return Ok(categories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryManager.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // GET: api/category/5/with-products
        [HttpGet("{id}/with-products")]
        public async Task<IActionResult> GetByIdWithProducts(int id)
        {
            var category = await _categoryManager.GetCategoryByIdWithProductsAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // GET: api/category/by-name/electronics
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _categoryManager.GetCategoryByNameAsync(name);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryManager.AddCategoryAsync(category);
            return Ok(category);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();
            category.Id = id;
            await _categoryManager.UpdateCategoryAsync(category);
            return NoContent();
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryManager.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}