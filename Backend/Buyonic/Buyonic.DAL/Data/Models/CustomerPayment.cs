using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL.Data.Models
{
    public class CustomerPayment
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        [ForeignKey("PaymentMethod")]
        public int paymentMethodId { get; set; }

        public Customer Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
