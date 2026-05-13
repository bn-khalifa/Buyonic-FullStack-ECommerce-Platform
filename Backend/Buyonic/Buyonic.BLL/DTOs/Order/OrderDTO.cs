using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
