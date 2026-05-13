namespace Buyonic.BLL
{
    public interface IOrderManager
    {
        Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId);
        Task<OrderDTO> GetOrderWithItemsAsync(int orderId);
        Task<OrderDTO> CreateOrderFromCartAsync(CreateOrderDTO dto);
        Task<OrderDTO> UpdateOrderStatusAsync(int orderId, string status);
    }
}
