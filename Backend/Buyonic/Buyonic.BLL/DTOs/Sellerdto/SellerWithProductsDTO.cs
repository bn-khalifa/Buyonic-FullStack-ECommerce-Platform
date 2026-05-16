namespace Buyonic.BLL;

 public class SellerWithProductsDTO
    {
        public int Id { get; set; }

        public string StoreName { get; set; } = null!;

        public decimal? Rating { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    
}
