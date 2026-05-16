using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        [ForeignKey("PaymentMethod")]
        public int paymentMethodId { get; set; }
        public string status { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deliveredAt { get; set; }
        public decimal totalAmount { get; set; }
        public string ShippingAddress { get; set; }

        public Customer Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
