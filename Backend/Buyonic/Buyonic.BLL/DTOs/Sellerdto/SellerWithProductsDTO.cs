using Buyonic.BLL.DTOs.Productdto;

namespace Buyonic.BLL.DTOs.Sellerdto;

public class SellerWithProductsDTO
{
    public int Id { get; set; }
    public string StoreName { get; set; }
    public float? Rating { get; set; }
    public IEnumerable<ProductDTO> Products { get; set; }
}
