using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.DTOs.Order
{
    public class CreateOrderDTO
    {
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public string ShippingAddress { get; set; }

        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
