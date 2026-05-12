using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BuyonicContext context) : base(context)
        {
        }

        // categories with products
        public async Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync()
        {
            return await _context.Categories
                .Include(c => c.products)
                .ToListAsync();
        }

        // get category by id
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // get category by name
        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.name == name);
        }
    }
}