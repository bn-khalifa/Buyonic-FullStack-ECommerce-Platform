namespace Buyonic.BLL
{
    public class CategoryWithProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
