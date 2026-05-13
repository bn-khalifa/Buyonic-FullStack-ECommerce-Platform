namespace Buyonic.DAL
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        public Task<bool> AddReviewAsync(Review review);
        public Task<List<Review>> GetAllReviewsAsync(int id);
    }
}
