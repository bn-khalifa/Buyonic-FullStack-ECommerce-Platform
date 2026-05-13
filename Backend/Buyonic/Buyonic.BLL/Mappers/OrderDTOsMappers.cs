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
            CreatedAt = o.createdAt,
            UpdatedAt = o.updatedAt,
            DeliveredAt = o.deliveredAt,
            TotalAmount = o.totalAmount,
            OrderItems = o.OrderItems.Select(OrderItemDtoMapper)
        };

        public static OrderItemDTO OrderItemDtoMapper(OrderItem i) => new OrderItemDTO
        {
            ProductId = i.productId,
            ProductName = i.Product.name,
            Price = i.price,
            Quantity = i.quantity
        };
    }
}
