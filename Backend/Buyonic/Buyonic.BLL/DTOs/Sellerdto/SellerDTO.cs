using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    // للـ POST
    public class CreateSellerDTO
    {
        [Required]
        [MaxLength(100)]
        public string StoreName { get; set; } = null!;

        [Required]
        public int UserId { get; set; }
    }

    // للـ PUT
    public class UpdateSellerDTO
    {
        [MaxLength(100)]
        public string? StoreName { get; set; }

        [Range(0, 5)]
        public decimal? Rating { get; set; }
    }

    // للـ GET
    public class SellerDTO
    {
        public int Id { get; set; }

        public string StoreName { get; set; } = null!;

        public decimal? Rating { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }

}