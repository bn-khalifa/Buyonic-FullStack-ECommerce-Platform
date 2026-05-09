using Microsoft.AspNetCore.Identity;

namespace Buyonic.DAL
{
    public class ApplicationUser : IdentityUser<int>, IAuditableEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        //public string role { get; set; }
        public bool isActive { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public bool isDeleted { get; set; } = false; // soft deletion

        public Customer? Customer { get; set; }
        public Seller? Seller { get; set; }

        //email, password, phone number will be inherited from IdentityUser base class
    }
}
