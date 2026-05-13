using Buyonic.BLL.DTOs.Productdto;

namespace Buyonic.BLL.DTOs.Categorydto
{
    public class CategoryWithProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
