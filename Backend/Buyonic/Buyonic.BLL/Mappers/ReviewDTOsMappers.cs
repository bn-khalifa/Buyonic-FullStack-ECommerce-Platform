using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class ReviewDTOsMappers
    {
        public static ProductReviewDTO ProductReviewDtoMapper(Review r) => new ProductReviewDTO
        {
            Id = r.Id,
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt,
            CustomerName = r.Customer?.User != null
                ? $"{r.Customer.User.firstName} {r.Customer.User.lastName}".Trim()
                : null
        };

        public static Review ReviewDtoMapper(ProductReviewDTO r) => new Review
        {
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
