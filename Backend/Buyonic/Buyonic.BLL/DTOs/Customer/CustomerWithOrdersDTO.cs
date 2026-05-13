using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class CustomerWithOrdersDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string? Address { get; set; }
        public IEnumerable<OrderDTO> Orders { get; set; }
    }
}
