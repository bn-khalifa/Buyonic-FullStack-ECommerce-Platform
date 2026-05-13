namespace Buyonic.DAL
{
    public class Seller
    {
        public int Id { get; set; }

        public string StoreName { get; set; } = null!;

        public decimal? Rating { get; set; }

        public int UserId { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public ApplicationUser? User { get; set; }
    }
}
