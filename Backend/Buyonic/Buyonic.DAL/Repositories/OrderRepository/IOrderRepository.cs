namespace Buyonic.DAL
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);

        Task<IEnumerable<Order>> GetOrdersBySellerIdAsync(int sellerId);

        Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync();

        Task<Order> GetOrderWithItemsAsync(int orderId);
    }
}
