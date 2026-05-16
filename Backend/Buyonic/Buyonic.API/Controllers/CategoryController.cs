using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Seller,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _categoryManager.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDTO category)
        {
            var result = await _categoryManager.GetCategoryByIdAsync(id);

            if (result == null)
                return NotFound();

            await _categoryManager.UpdateCategoryAsync(id, category);

            return NoContent();
        }

        // DELETE: api/category/5
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryManager.GetCategoryByIdAsync(id);

            if (result == null)
                return NotFound();

            await _categoryManager.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}