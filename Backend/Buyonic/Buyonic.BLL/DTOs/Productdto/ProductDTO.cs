using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class CreateProductDTO
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = null!;

        [Range(1, 1000000)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public decimal Discount { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SellerId { get; set; }
    }

    public class UpdateProductDTO
    {
        [MaxLength(200)]
        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Range(1, 1000000)]
        public decimal? Price { get; set; }

        [Range(0, 100)]
        public decimal? Discount { get; set; }

        [Range(0, int.MaxValue)]
        public int? StockQuantity { get; set; }

        public int? CategoryId { get; set; }

        public int? SellerId { get; set; }
    }

    //Get

    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal? Rating { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }

        public int SellerId { get; set; }

        public int ReviewCount { get; set; }
    }

}