using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = null!;
    }
}
