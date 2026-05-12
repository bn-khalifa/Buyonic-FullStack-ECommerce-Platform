using Buyonic.BLL.DTOs.Cart;
using Buyonic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.Mappers
{
    public class CartDTOsMappers
    {
        public static CartDTO CartDtoMapper(Cart c) => new CartDTO
        {
            Id = c.Id,
            CustomerId = c.customerId,
            CartItems = c.CartItems.Select(CartItemDtoMapper)
        };

        public static CartItemDTO CartItemDtoMapper(CartItem i) => new CartItemDTO
        {
            ProductId = i.productId,
            ProductName = i.Product.name,
            ProductPrice = i.Product.price,
            Quantity = i.quantity
        };
    }
}
