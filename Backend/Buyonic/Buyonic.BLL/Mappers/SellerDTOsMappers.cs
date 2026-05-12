using Buyonic.BLL.DTOs.Sellerdto;
using Buyonic.BLL.Mappers;
using Buyonic.DAL;

namespace Buyonic.BLL.Mappers // ← غيري من Buyonic.BLL
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
            Products = s.Products.Select(ProductDTOsMappers.ProductDtoMapper)
        };
    }
}