using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(BuyonicContext context) : base(context) { }

        public async Task<Wishlist?> GetWishlistByCustomerIdAsync(int customerId)
        {
            return await _context.Wishlists
                .Include(w => w.WishlistItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(w => w.customerId == customerId);
        }

        public async Task<WishlistItem?> GetWishlistItemAsync(int wishlistId, int productId)
        {
            return await _context.WishlistItems
                .FirstOrDefaultAsync(i => i.wishlistId == wishlistId && i.productId == productId);
        }

        public async Task RemoveItemAsync(WishlistItem item)
        {
            _context.WishlistItems.Remove(item);
        }

        public async Task ClearWishlistAsync(int wishlistId)
        {
            var items = await _context.WishlistItems
                .Where(i => i.wishlistId == wishlistId)
                .ToListAsync();
            _context.WishlistItems.RemoveRange(items);
        }

        public void AddItemAsync(WishlistItem item)
        {
             _context.WishlistItems.Add(item);
        }
    }
}