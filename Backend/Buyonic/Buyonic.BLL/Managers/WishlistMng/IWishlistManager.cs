namespace Buyonic.BLL
{
    public interface IWishlistManager
    {
        Task<WishlistDTO?> GetWishlistAsync(int customerId);
        Task<bool> AddToWishlistAsync(int customerId, int productId);
        Task<bool> RemoveFromWishlistAsync(int customerId, int productId);
        Task<bool> ClearWishlistAsync(int customerId);
    }
}