using Buyonic.BLL.DTOs.Productdto;

using Buyonic.DAL;

namespace Buyonic.BLL.Mappers
{
    public class ProductDTOsMappers
    {
        public static ProductDTO ProductDtoMapper(Product p) => new ProductDTO
        {
            Id = p.Id,
            Name = p.name,
            ImageUrl = p.imageUrl,
            Price = p.price,
            Discount = p.discount,
            Rating = p.rating,
            StockQuantity = p.stockQuantity,
            Description = p.description
        };

        public static ProductWithSellerDTO ProductWithSellerDtoMapper(Product p) => new ProductWithSellerDTO
        {
            Id = p.Id,
            Name = p.name,
            ImageUrl = p.imageUrl,
            Price = p.price,
            Discount = p.discount,
            Rating = p.rating,
            StockQuantity = p.stockQuantity,
            Description = p.description,
            SellerStoreName = p.Seller.storeName
        };
        public static ProductWithCategoryDTO ProductWithCategoryDtoMapper(Product p) => new ProductWithCategoryDTO
        {
            Id = p.Id,
            Name = p.name,
            ImageUrl = p.imageUrl,
            Price = p.price,
            Discount = p.discount,
            Rating = p.rating,
            StockQuantity = p.stockQuantity,
            Description = p.description,
            CategoryName = p.Category.name
        };

    }
}
