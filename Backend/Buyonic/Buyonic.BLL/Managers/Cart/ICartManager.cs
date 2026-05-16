using Buyonic.BLL.DTOs.Cart;

namespace Buyonic.BLL
{
    public interface ICartManager
    {
        //Task<CartDTO> GetCartByCustomerIdAsync(int customerId);
        Task<CartDTO> GetCartByEmailAsync(string customerEmail);
        Task<CartDTO> GetCartWithItemsAsync(int cartId);
        //Task AddToCartAsync(int customerId, int productId, int quantity);
        Task AddToCartAsync(string customerEmail, int productId, int quantity);
        //Task UpdateCartItemAsync(int cartId, int productId, int quantity);
        Task UpdateCartItemAsync(string customerEmail, int productId, int quantity);
        //Task RemoveFromCartAsync(int cartId, int productId);
        Task RemoveFromCartAsync(string customerEmail, int productId);
        //Task ClearCartAsync(int cartId);
        Task ClearCartAsync(string customerEmail);
    }
}