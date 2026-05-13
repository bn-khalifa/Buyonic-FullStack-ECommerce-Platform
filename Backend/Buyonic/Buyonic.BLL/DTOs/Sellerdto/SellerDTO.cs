namespace Buyonic.BLL
{
    // للـ POST
    public class CreateSellerDTO
    {
        public string StoreName { get; set; }
        public int UserId { get; set; }
    }

    // للـ PUT
    public class UpdateSellerDTO
    {
        public string? storeName { get; set; }
        public float? rating { get; set; }
    }

    // للـ GET
    public class SellerDTO
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public float? Rating { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

}