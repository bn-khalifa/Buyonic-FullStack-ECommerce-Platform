using Buyonic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository
                .GetAllAsync();

            return Ok(categories);
        }

        // GET: api/category/with-products
        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllWithProducts()
        {
            var categories = await _unitOfWork.CategoryRepository
                .GetAllCategoriesWithProductsAsync();

            return Ok(categories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // GET: api/category/by-name/electronics
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetCategoryByNameAsync(name);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            _unitOfWork.CategoryRepository.Add(category);

            await _unitOfWork.SaveAsync();

            return Ok(category);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            _unitOfWork.CategoryRepository.Update(category);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetByIdAsync(id);

            if (category == null)
                return NotFound();

            _unitOfWork.CategoryRepository.Delete(category);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}