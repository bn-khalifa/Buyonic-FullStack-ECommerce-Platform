using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? picture { get; set; }
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public float discount { get; set; } = 0;
        public int stockQuantity { get; set; }
        public float? rating { get; set; }
        [ForeignKey("Seller")]
        public int sellerId { get; set; }
        [ForeignKey("Category")]
        public int categoryId { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public bool isDeleted { get; set; } = false;

        public Category Category { get; set; }
        public Seller Seller { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}
