using Buyonic.DAL.Data.Models;

namespace Buyonic.DAL
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string methodName { get; set; }

        public ICollection<CustomerPayment> CustomerPayments { get; set; } = new List<CustomerPayment>();
        public ICollection<Order> Orders = new List<Order>();
    }
}
