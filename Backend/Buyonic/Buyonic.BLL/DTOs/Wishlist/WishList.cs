namespace Buyonic.BLL
{
    public class WishlistDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<WishlistItemDTO> Items { get; set; } = new List<WishlistItemDTO>();
    }

    public class WishlistItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? Rating { get; set; }
        public decimal Discount { get; set; }
    }

    public class AddToWishlistDTO
    {
        public int ProductId { get; set; }
    }
}