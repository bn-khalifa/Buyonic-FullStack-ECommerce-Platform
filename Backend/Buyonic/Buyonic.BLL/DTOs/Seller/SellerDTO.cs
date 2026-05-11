namespace Buyonic.BLL
{
    public class SellerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? StoreName { get; set; }
        public float? Rating { get; set; }
    }
}