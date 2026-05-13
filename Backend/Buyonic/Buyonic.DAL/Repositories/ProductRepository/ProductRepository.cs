using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(BuyonicContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithSellersAsync()
        {
            return await _context.Products
                .Where(p => !p.isDeleted)
                .Include(p => p.Seller)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await _context.Products
                .Where(p => !p.isDeleted)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => !p.isDeleted)
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => !p.isDeleted && p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId)
        {
            return await _context.Products
                .Where(p => !p.isDeleted && p.SellerId == sellerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => !p.isDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithOrderItemsAsync()
        {
            return await _context.Products
        .Where(p => !p.isDeleted)
        .Include(p => p.OrderItems)
        .ToListAsync();
        }
    }
}