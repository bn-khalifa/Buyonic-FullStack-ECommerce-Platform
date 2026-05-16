namespace Buyonic.DAL
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        public Task<bool> AddReviewAsync(Review review);
        public Task<List<Review>> GetAllReviewsAsync(int id);
        Task<bool> HasReviewAsync(int customerId, int productId);
    }
}
