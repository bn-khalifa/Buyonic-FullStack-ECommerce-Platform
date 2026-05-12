using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.DTOs.Cart
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<CartItemDTO> CartItems { get; set; }
        public decimal TotalPrice => CartItems?.Sum(i => i.SubTotal) ?? 0;
    }
}
