using System.ComponentModel.DataAnnotations.Schema;

namespace Buyonic.DAL
{
    public class Review
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }

    }
}
