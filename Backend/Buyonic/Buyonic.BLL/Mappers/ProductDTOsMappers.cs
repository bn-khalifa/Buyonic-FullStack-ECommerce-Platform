using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class ProductDTOsMappers
    {
        public static ProductDTO ProductDtoMapper(Product p)
        {
            return new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Discount = p.Discount,
                Rating = p.Rating,
                StockQuantity = p.StockQuantity,
                Description = p.Description,
                CategoryId = p.CategoryId,
                SellerId = p.SellerId,
                ReviewCount = p.ReviewCount
            };
        }

        public static ProductWithSellerDTO ProductWithSellerDtoMapper(Product p)
        {
            return new ProductWithSellerDTO
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Discount = p.Discount,
                Rating = p.Rating,
                StockQuantity = p.StockQuantity,
                Description = p.Description,
                SellerStoreName = p.Seller!.StoreName
            };
        }

        public static ProductWithCategoryDTO ProductWithCategoryDtoMapper(Product p)
        {
            return new ProductWithCategoryDTO
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Discount = p.Discount,
                Rating = p.Rating,
                StockQuantity = p.StockQuantity,
                Description = p.Description,
                CategoryName = p.Category!.Name
            };
        }
    }
}