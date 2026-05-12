using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        public SellerRepository(BuyonicContext context) : base(context)
        {
        }
        
        // sellers with products
        public async Task<IEnumerable<Seller>> GetAllSellersWithProductsAsync()
        {
            return await _context.Sellers
                .Include(s => s.Products)
                .ToListAsync();
        }

        public async Task<Seller> GetSellerByEmailAsync(string email)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User.Email == email);
        }
        // get seller by id

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            return await _context.Sellers
                .Include(s => s.Products)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // get seller by store name
        public async Task<Seller> GetSellerByStoreNameAsync(string storeName)
        {
            return await _context.Sellers
                .FirstOrDefaultAsync(s => s.storeName == storeName);
        }

        
        public async Task<IEnumerable<Seller>> GetAllSellersAsync()
        {
            return await _context.Sellers
                .Include(s => s.User)
                .ToListAsync();
        }

    }
}