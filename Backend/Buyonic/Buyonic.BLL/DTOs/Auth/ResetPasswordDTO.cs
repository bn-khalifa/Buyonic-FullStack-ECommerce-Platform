using System.ComponentModel.DataAnnotations;

namespace Buyonic.BLL
{
    public class ResetPasswordDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}