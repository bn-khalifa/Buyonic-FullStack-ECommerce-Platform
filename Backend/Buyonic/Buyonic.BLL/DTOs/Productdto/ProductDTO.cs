namespace Buyonic.BLL;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; } 
    public decimal Price { get; set; }
    public float Discount { get; set; }
    public float? Rating { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; }

    public int CategoryId { get; set; }

    public int SellerId { get; set; }
}
