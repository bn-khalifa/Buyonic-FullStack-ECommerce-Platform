using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class ReviewManager : IReviewManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductReviewDTO>> GetProductReviews(int productId)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllReviewsAsync(productId);
            return reviews.Select(ReviewDTOsMappers.ProductReviewDtoMapper);
        }

        public async Task<bool> SubmitReviewAsync(ProductReviewDTO dto)
        {
            // Validate customer has a delivered order containing this product
            var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerIdAsync(dto.CustomerId);
            var hasDeliveredOrder = orders.Any(o =>
                o.status == "Delivered" &&
                o.OrderItems != null &&
                o.OrderItems.Any(i => i.productId == dto.ProductId));

            if (!hasDeliveredOrder) return false;

            // Update product rating
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
            if (product == null) return false;

            product.rating = ((product.rating * product.ReviewCount) + dto.Rating) / (product.ReviewCount + 1);
            product.ReviewCount++;
            _unitOfWork.ProductRepository.Update(product);

            // Add review
            var review = ReviewDTOsMappers.ReviewDtoMapper(dto);
            await _unitOfWork.ReviewRepository.AddReviewAsync(review);

            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
