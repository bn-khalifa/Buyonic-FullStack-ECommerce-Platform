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

        public async Task<ReviewEligibilityDTO> GetReviewEligibilityAsync(int customerId, int productId)
        {
            var hasReviewed = await _unitOfWork.ReviewRepository.HasReviewAsync(customerId, productId);
            if (hasReviewed)
            {
                return new ReviewEligibilityDTO
                {
                    CanReview = false,
                    HasReviewed = true,
                    Message = "You have already reviewed this product."
                };
            }

            var hasDeliveredOrder = await HasDeliveredPurchaseAsync(customerId, productId);
            if (!hasDeliveredOrder)
            {
                return new ReviewEligibilityDTO
                {
                    CanReview = false,
                    HasReviewed = false,
                    Message = "You can review this product after your order has been delivered."
                };
            }

            return new ReviewEligibilityDTO
            {
                CanReview = true,
                HasReviewed = false,
                Message = null
            };
        }

        public async Task<ProductDTO?> SubmitReviewAsync(ProductReviewDTO dto)
        {
            var eligibility = await GetReviewEligibilityAsync(dto.CustomerId, dto.ProductId);
            if (!eligibility.CanReview) return null;

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
            if (product == null) return null;

            if (product.ReviewCount == 0)
                product.Rating = dto.Rating;
            else
                product.Rating = ((product.Rating * product.ReviewCount) + dto.Rating) / (product.ReviewCount + 1);

            product.ReviewCount++;
            _unitOfWork.ProductRepository.Update(product);

            var review = ReviewDTOsMappers.ReviewDtoMapper(dto);
            await _unitOfWork.ReviewRepository.AddReviewAsync(review);

            await _unitOfWork.SaveAsync();

            var updated = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
            return updated == null ? null : ProductDTOsMappers.ProductDtoMapper(updated);
        }

        private async Task<bool> HasDeliveredPurchaseAsync(int customerId, int productId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerIdAsync(customerId);
            return orders.Any(o =>
                o.status == "Delivered" &&
                o.OrderItems != null &&
                o.OrderItems.Any(i => i.productId == productId));
        }
    }
}
