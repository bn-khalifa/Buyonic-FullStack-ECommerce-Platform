namespace Buyonic.DAL
{
    public class Customer
    {
        public int Id { get; set; }
        public string? address { get; set; }
        public int userId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
