using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class CartItem
    {
        public int Id { get; set; }
        [ForeignKey("Cart")]
        public int cartId { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        public int quantity { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
