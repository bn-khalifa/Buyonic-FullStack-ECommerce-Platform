using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class ProductReviewDTO
    {
        public int? Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required, Range(1, 5)]
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CustomerName { get; set; }
    }
}