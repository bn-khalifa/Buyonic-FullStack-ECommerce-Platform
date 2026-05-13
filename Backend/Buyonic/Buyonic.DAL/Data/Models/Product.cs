using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal Discount { get; set; } = 0;

        public int StockQuantity { get; set; }

        public decimal? Rating { get; set; }

        [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime? updatedAt { get; set; }
       
        public bool isDeleted { get; set; } = false;
        
        public int ReviewCount { get; set; } = 0;

        public Category? Category { get; set; }

        public Seller? Seller { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}