using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _uniteOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            _uniteOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _uniteOfWork.OrderRepository.GetOrdersByCustomerIdAsync(customerId);
            return orders.Select(OrderDTOsMappers.OrderDtoMapper);
        }

        public async Task<OrderDTO> GetOrderWithItemsAsync(int orderId)
        {
            var order = await _uniteOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);
            if (order == null) return null;

            return OrderDTOsMappers.OrderDtoMapper(order);
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto)
        {
            if (dto.OrderItems == null || !dto.OrderItems.Any())
                throw new InvalidOperationException("Order must have at least one item.");

            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in dto.OrderItems)
            {
                var product = await _uniteOfWork.ProductRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

                total += product.price * item.Quantity;

                orderItems.Add(new OrderItem
                {
                    productId = item.ProductId,
                    quantity = item.Quantity,
                    price = product.price
                });
            }

            var order = new global::Buyonic.DAL.Order
            {
                customerId = dto.CustomerId,
                paymentMethodId = dto.PaymentMethodId,
                ShippingAddress = dto.ShippingAddress,
                status = "Pending",
                createdAt = DateTime.UtcNow,
                totalAmount = total,
                OrderItems = orderItems
            };

            _uniteOfWork.OrderRepository.Add(order);
            await _uniteOfWork.SaveAsync();

            var createdOrder = await _uniteOfWork.OrderRepository.GetOrderWithItemsAsync(order.Id);
            return OrderDTOsMappers.OrderDtoMapper(createdOrder);
        }

        public async Task<OrderDTO> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _uniteOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);
            if (order == null) return null;

            order.status = status;
            order.updatedAt = DateTime.UtcNow;

            if (status == "Delivered")
                order.deliveredAt = DateTime.UtcNow;

            await _uniteOfWork.SaveAsync();

            return OrderDTOsMappers.OrderDtoMapper(order);
        }
    }
}



//    public class OrderManager : IOrderManager
//    {
//        private readonly IUnitOfWork _uniteOfWork;

//        public OrderManager(IUnitOfWork unitOfWork)
//        {
//            _uniteOfWork = unitOfWork;
//        }
//        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId)
//        {
//            var orders = await _uniteOfWork.OrderRepository.GetOrdersByCustomerIdAsync(customerId);
//            return orders.Select(OrderDTOsMappers.OrderDtoMapper);
//        }


//        public async Task<OrderDTO> GetOrderWithItemsAsync(int orderId)
//        {
//            var order = await _uniteOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);
//            if (order == null) return null;

//            return OrderDTOsMappers.OrderDtoMapper(order);
//        }

//        public async Task<OrderDTO> CreateOrderFromCartAsync(CreateOrderDTO dto)
//        {
//            // جيب الكارت بالـ customerId الاول
//            var cartByCustomer = await _uniteOfWork.CartRepository.GetCartByCustomerIdAsync(dto.CustomerId);
//            if (cartByCustomer == null)
//                throw new InvalidOperationException("Cart is empty or not found.");

//            // جيب الكارت مع الـ items بالـ cartId
//            var cart = await _uniteOfWork.CartRepository.GetCartWithItemsAsync(cartByCustomer.Id);
//            if (cart == null || !cart.CartItems.Any())
//                throw new InvalidOperationException("Cart is empty or not found.");

//            // احسب الـ total من الـ items
//            var total = cart.CartItems.Sum(i => i.Product.price * i.quantity);

//            // ابعت الـ Order
//            var order = new global::Buyonic.DAL.Order
//            {
//                customerId = dto.CustomerId,
//                paymentMethodId = dto.PaymentMethodId,
//                status = "Pending",
//                createdAt = DateTime.UtcNow,
//                totalAmount = total,
//                OrderItems = cart.CartItems.Select(i => new OrderItem
//                {
//                    productId = i.productId,
//                    quantity = i.quantity,
//                    price = i.Product.price
//                }).ToList()
//            };

//            _uniteOfWork.OrderRepository.Add(order);

//            // فضي الكارت بعد ما الـ Order اتعملت
//            //cart.CartItems.Clear();

//            foreach (var item in cart.CartItems.ToList())
//                _uniteOfWork.CartRepository.DeleteCartItem(item);

//            await _uniteOfWork.SaveAsync();

//            // رجّع الـ Order مع الـ items
//            var createdOrder = await _uniteOfWork.OrderRepository.GetOrderWithItemsAsync(order.Id);
//            return OrderDTOsMappers.OrderDtoMapper(createdOrder);
//        }


//        public async Task<OrderDTO> UpdateOrderStatusAsync(int orderId, string status)
//        {
//            var order = await _uniteOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);
//            if (order == null) return null;

//            order.status = status;
//            order.updatedAt = DateTime.UtcNow;

//            if (status == "Delivered")
//                order.deliveredAt = DateTime.UtcNow;

//            await _uniteOfWork.SaveAsync();

//            return OrderDTOsMappers.OrderDtoMapper(order);
//        }
//    }
//}
