using Buyonic.DAL;

namespace Buyonic.BLL
{
    public interface IReviewManager
    {
        public Task<bool> SubmitReviewAsync(ProductReviewDTO review);
        public Task<IEnumerable<ProductReviewDTO>> GetProductReviews(int id);
    }
}
