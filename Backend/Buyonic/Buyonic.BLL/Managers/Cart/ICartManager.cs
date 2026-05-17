using Buyonic.BLL.DTOs.Cart;

namespace Buyonic.BLL
{
    public interface ICartManager
    {
        Task<CartDTO> GetCartByEmailAsync(string customerEmail);
        Task<CartDTO> GetCartWithItemsAsync(int cartId);
        Task AddToCartAsync(string customerEmail, int productId, int quantity);
        Task UpdateCartItemAsync(string customerEmail, int productId, int quantity);
        Task RemoveFromCartAsync(string customerEmail, int productId);
        Task ClearCartAsync(string customerEmail);
    }
}