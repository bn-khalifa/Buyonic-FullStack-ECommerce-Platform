
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomerRepository(BuyonicContext context, 
                                  UserManager<ApplicationUser> userManager) : base(context) 
        {
            _userManager = userManager;
        }

        // returns customers and their orders
        public async Task<IEnumerable<Customer>> GetAllCustomersWithOrdersAsync()
        {
            var customers = await _context.Customers.Include(c => c.Orders).ToListAsync();
            return customers;
        }

        // returns customers and their carts
        public async Task<IEnumerable<Customer>> GetAllCustomersWithCartsAsync()
        {
            var customers = await _context.Customers.Include(c => c.Cart).ToListAsync();
            return customers;
        }

        // return customers along with thier wishlists
        public async Task<IEnumerable<Customer>> GetAllCustomersWithWishlistsAsync()
        {
            var customers = await _context.Customers.Include(c => c.Wishlist).ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public new async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == email);
        }
    }
}
