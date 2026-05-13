using Buyonic.BLL.DTOs.Cart;

namespace Buyonic.BLL
{
    public interface ICartManager
    {
        Task<CartDTO> GetCartByCustomerIdAsync(int customerId);
        Task<CartDTO> GetCartWithItemsAsync(int cartId);
        Task AddToCartAsync(int customerId, int productId, int quantity);
        Task UpdateCartItemAsync(int cartId, int productId, int quantity);
        Task RemoveFromCartAsync(int cartId, int productId);
        Task ClearCartAsync(int cartId);
    }
}
