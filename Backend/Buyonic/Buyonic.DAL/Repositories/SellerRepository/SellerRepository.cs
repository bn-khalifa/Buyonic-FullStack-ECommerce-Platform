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

        // get seller by id
        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            return await _context.Sellers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // get seller by store name
        public async Task<Seller> GetSellerByStoreNameAsync(string storeName)
        {
            return await _context.Sellers
                .FirstOrDefaultAsync(s => s.storeName == storeName);
        }

        

    }
}