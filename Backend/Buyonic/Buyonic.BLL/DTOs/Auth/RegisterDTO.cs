using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required, RegularExpression("Customer|customer|Seller|seller|Admin|admin")]
        public string AccountType { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
    }
}
