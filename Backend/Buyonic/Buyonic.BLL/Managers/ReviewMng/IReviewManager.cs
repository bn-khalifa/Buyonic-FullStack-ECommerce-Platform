using Buyonic.DAL;

namespace Buyonic.BLL
{
    public interface IReviewManager
    {
        Task<ProductDTO?> SubmitReviewAsync(ProductReviewDTO review);
        public Task<IEnumerable<ProductReviewDTO>> GetProductReviews(int id);
        Task<ReviewEligibilityDTO> GetReviewEligibilityAsync(int customerId, int productId);
    }
}
