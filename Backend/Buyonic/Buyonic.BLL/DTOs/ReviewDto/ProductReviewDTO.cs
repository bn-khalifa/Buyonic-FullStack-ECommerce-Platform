using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class ProductReviewDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required, Range(1, 5)]
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}