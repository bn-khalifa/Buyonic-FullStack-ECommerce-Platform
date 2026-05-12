using Buyonic.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.Managers.Order
{
    public interface IOrderManager
    {
        Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId);
        Task<OrderDTO> GetOrderWithItemsAsync(int orderId);
        Task<OrderDTO> CreateOrderFromCartAsync(CreateOrderDTO dto);
        Task<OrderDTO> UpdateOrderStatusAsync(int orderId, string status);
    }
}
