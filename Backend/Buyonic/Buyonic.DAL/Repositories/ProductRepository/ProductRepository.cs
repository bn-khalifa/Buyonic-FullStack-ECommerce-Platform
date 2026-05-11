using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(BuyonicContext context) : base(context)
        {
        }

        // products with sellers
        public async Task<IEnumerable<Product>> GetAllProductsWithSellersAsync()
        {
            return await _context.Products
                .Include(p => p.Seller)
                .ToListAsync();
        }

        // products with categories
        public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        // products with order items
        public async Task<IEnumerable<Product>> GetAllProductsWithOrderItemsAsync()
        {
            return await _context.Products
                .Include(p => p.OrderItems)
                .ToListAsync();
        }

        // get product by id
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // products by category
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.categoryId == categoryId)
                .ToListAsync();
        }

        // products by seller
        public async Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId)
        {
            return await _context.Products
                .Where(p => p.sellerId == sellerId)
                .ToListAsync();
        }
    }
}