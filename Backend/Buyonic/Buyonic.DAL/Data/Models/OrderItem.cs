using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class OrderItem
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        [ForeignKey("Order")]
        public int orderId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
