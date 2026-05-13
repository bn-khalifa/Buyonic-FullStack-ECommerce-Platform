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
        public decimal Price { get; set; }
        public float? Rating { get; set; }
        public float Discount { get; set; }
    }

    public class AddToWishlistDTO
    {
        public int ProductId { get; set; }
    }
}