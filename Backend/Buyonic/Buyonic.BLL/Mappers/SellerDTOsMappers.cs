using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class SellerDTOsMappers
    {
        public static SellerDTO SellerDtoMapper(Seller s) => new SellerDTO
        {
            Id = s.Id,
            StoreName = s.storeName,
            Rating = s.rating,
            FirstName = s.User.firstName,
            LastName = s.User.lastName,
            Email = s.User.Email!
        };

        public static SellerWithProductsDTO SellerWithProdDtoMapper(Seller s) => new SellerWithProductsDTO
        {
            Id = s.Id,
            StoreName = s.storeName,
            Rating = s.rating,
            Products = s.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.name,
                Price = p.price,
                Discount = p.discount,
                Rating = p.rating,
                StockQuantity = p.stockQuantity
            })
        };
    }
}
