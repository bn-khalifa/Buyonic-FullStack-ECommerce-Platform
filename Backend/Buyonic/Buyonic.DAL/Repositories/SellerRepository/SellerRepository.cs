using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerRepository(
            BuyonicContext context,
            UserManager<ApplicationUser> userManager
        ) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Seller>> GetAllSellersAsync()
        {
            return await _context.Sellers
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Seller>> GetAllSellersWithProductsAsync()
        {
            return await _context.Sellers
                .Include(s => s.User)
                .Include(s => s.Products)
                .ToListAsync();
        }

        public async Task<Seller?> GetSellerByIdAsync(int id)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Seller?> GetSellerByEmailAsync(string email)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User!.Email == email);
        }

        public async Task<Seller?> GetSellerByUserIdAsync(int userId)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<Seller?> GetSellerWithProductsByIdAsync(int id)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Seller?> GetSellerWithProductsByEmailAsync(string email)
        {
            return await _context.Sellers
                .Include(s => s.User)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.User!.Email == email);
        }

        public async Task<Seller?> GetSellerByStoreNameAsync(string storeName)
        {
            return await _context.Sellers
                .FirstOrDefaultAsync(s => s.StoreName == storeName);
        }
    }
}