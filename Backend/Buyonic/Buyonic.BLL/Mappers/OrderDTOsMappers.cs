using Buyonic.BLL.DTOs.Order;
using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class OrderDTOsMappers
    {
        public static OrderDTO OrderDtoMapper(Order o) => new OrderDTO
        {
            Id = o.Id,
            CustomerId = o.customerId,
            PaymentMethodId = o.paymentMethodId,
            Status = o.status,
            ShippingAddress = o.ShippingAddress,
            CreatedAt = o.createdAt,
            UpdatedAt = o.updatedAt,
            DeliveredAt = o.deliveredAt,
            TotalAmount = o.totalAmount,
            OrderItems = o.OrderItems.Select(OrderItemDtoMapper)
        };

        public static OrderItemDTO OrderItemDtoMapper(OrderItem i) => new OrderItemDTO
        {
            ProductId = i.productId,
            ProductName = i.Product.Name,
            Price = i.price,
            Quantity = i.quantity
        };
    }
}
