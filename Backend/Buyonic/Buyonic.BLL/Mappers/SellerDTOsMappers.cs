using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class SellerDTOsMappers
    {
        public static SellerDTO SellerDtoMapper(Seller s)
        {
            return new SellerDTO
            {
                Id = s.Id,
                StoreName = s.StoreName,
                Rating = s.Rating,

                FirstName = s.User!.firstName,
                LastName = s.User.lastName,
                Email = s.User.Email!
            };
        }

        public static SellerWithProductsDTO SellerWithProdDtoMapper(Seller s)
        {
            return new SellerWithProductsDTO
            {
                Id = s.Id,
                StoreName = s.StoreName,
                Rating = s.Rating,

                FirstName = s.User!.firstName,
                LastName = s.User.lastName,
                Email = s.User.Email!,

                Products = s.Products.Select(ProductDTOsMappers.ProductDtoMapper)
            };
        }
    }
}