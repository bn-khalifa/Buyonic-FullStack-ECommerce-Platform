
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
            return await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                .ToListAsync();
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

        public async Task<bool> DeleteCustomerByID(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer is null) return false;

            var user = await _userManager.FindByIdAsync(customer.userId.ToString());
            if (user is null) return false;

            user.isDeleted = true;
            user.isActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
