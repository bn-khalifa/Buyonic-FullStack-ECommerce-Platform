namespace Buyonic.DAL
{
    public class Seller
    {
        public int Id { get; set; }
        public string? storeName { get; set; }
        public float? rating { get; set; }
        public int userId { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public ApplicationUser User { get; set; }
    }
}
