namespace Buyonic.BLL;

public class SellerWithProductsDTO
{
    public int Id { get; set; }
    public string? StoreName { get; set; }
    public float? Rating { get; set; }
    public IEnumerable<ProductDTO> Products { get; set; }
}
