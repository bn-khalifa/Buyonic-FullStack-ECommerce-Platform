using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Repositories.CartRepository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetCartByCustomerIdAsync(int customerId);

        Task<Cart> GetCartWithItemsAsync(int cartId);

        Task<CartItem> GetCartItemAsync(int cartId, int productId);
    }
}
