using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BuyonicContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.customerId == customerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.createdAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersBySellerIdAsync(int sellerId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.OrderItems.Any(oi => oi.Product.SellerId == sellerId))
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.createdAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.createdAt)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithItemsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
