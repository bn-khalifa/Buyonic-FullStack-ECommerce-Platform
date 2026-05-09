using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class Wishlist
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();

    }
}
