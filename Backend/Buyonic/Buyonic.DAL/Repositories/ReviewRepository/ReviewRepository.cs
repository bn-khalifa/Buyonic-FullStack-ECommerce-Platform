using Buyonic.DAL;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(BuyonicContext context) : base(context) { }

    public async Task<bool> AddReviewAsync(Review review)
    {
        Add(review);
        return true;
    }

    public async Task<List<Review>> GetAllReviewsAsync(int productId)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId)
            .Include(r => r.Customer)
            .ThenInclude(c => c.User)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> HasReviewAsync(int customerId, int productId)
    {
        return await _context.Reviews
            .AnyAsync(r => r.CustomerId == customerId && r.ProductId == productId);
    }
}