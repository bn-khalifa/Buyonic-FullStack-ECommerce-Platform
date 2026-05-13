namespace Buyonic.BLL
{
    public class ProductWithCategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Discount { get; set; }

        public decimal? Rating { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; } = null!;

        public string CategoryName { get; set; } = null!;
    }
}
