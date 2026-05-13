using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SellerRepository(BuyonicContext context,
                                UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public new async Task<IEnumerable<Seller>> GetAllAsync()
        // sellers with products

        {
            return await _context.Sellers
                .Include(s => s.User)
                .Include(s=>s.Products)
                .ToListAsync();
        }

        public async Task<Seller> GetSellerByEmailAsync(string email)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User.Email == email);
        }
        // get seller by id

        public async Task<IEnumerable<Seller>> GetAllSellersWithProductsAsync()
        {
            return await _context.Sellers
                .Include(s => s.Products)
                .Include(s => s.User)
                .ToListAsync();
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

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            var seller = await _context.Sellers.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
            return seller;
        }
    }
}