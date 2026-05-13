namespace Buyonic.DAL
{
    public interface IWishlistRepository : IGenericRepository<Wishlist>
    {
        Task<Wishlist?> GetWishlistByCustomerIdAsync(int customerId);
        Task<WishlistItem?> GetWishlistItemAsync(int wishlistId, int productId);
        void AddItemAsync(WishlistItem item);
        Task RemoveItemAsync(WishlistItem item);
        Task ClearWishlistAsync(int wishlistId);
    }
}