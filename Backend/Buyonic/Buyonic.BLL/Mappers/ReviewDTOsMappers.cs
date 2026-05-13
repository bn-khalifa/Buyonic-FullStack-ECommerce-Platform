using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class ReviewDTOsMappers
    {
        public static ProductReviewDTO ProductReviewDtoMapper(Review r) => new ProductReviewDTO
        {
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            Rating = r.Rating,
            Comment = r.Comment
        };

        public static Review ReviewDtoMapper(ProductReviewDTO r) => new Review
        {
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = DateTime.Now,
        };
    }
}
