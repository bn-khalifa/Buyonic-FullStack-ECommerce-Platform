namespace Buyonic.DAL
{
    public class Category
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public ICollection<Product>?products { get; set; } = new List<Product>();
    }
}
