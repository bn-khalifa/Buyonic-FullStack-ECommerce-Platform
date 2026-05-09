using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class WishlistItem
    {
        public int Id { get; set; }
        [ForeignKey("Wishlist")]
        public int wishlistId { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }

        public Wishlist Wishlist { get; set; }
        public Product Product { get; set; }
    }
}
