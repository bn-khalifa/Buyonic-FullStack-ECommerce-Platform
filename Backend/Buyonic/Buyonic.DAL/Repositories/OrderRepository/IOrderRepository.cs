namespace Buyonic.DAL
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);

        Task<Order> GetOrderWithItemsAsync(int orderId);
    }
}
