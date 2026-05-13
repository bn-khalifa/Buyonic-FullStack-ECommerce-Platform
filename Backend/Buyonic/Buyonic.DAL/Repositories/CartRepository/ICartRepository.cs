namespace Buyonic.DAL
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetCartByCustomerIdAsync(int customerId);

        Task<Cart> GetCartWithItemsAsync(int cartId);

        Task<CartItem> GetCartItemAsync(int cartId, int productId);

        void DeleteCartItem(CartItem cartItem);
    }
}
