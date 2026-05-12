namespace Buyonic.BLL
{
    namespace Buyonic.BLL
    {
        public class CustomerDTO
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string? Address { get; set; }
            public DateTime? JoinedAt { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
