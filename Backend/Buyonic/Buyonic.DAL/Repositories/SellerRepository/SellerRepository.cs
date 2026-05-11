using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Repositories
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
        {
            return await _context.Sellers
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Seller>> GetAllSellersWithProductsAsync()
        {
            var sellers = await _context.Sellers.Include(s => s.Products).ToListAsync();
            return sellers;
        }

        public async Task<Seller> GetSellerByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { 
                return null; 
            }
            
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.userId == user.Id);
            return seller;
        }

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            var seller = await _context.Sellers.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
            return seller;
        }
    }
}
