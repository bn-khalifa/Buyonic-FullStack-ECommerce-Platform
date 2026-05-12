namespace Buyonic.BLL.DTOs.Productdto
{
    public class ProductWithCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public float Discount { get; set; }
        public float? Rating { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}
