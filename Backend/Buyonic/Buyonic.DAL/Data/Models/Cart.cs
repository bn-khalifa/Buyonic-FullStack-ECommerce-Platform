using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
