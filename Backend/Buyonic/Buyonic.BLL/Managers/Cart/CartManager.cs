using Buyonic.BLL.DTOs.Cart;
using Buyonic.BLL.Mappers;
using Buyonic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.Managers.Cart
{

    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _uniteOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _uniteOfWork = unitOfWork;
        }

        public async Task<CartDTO> GetCartByCustomerIdAsync(int customerId)
        {
            var cart = await _uniteOfWork.CartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null) return null;

            return CartDTOsMappers.CartDtoMapper(cart);
        }

        public async Task<CartDTO> GetCartWithItemsAsync(int cartId)
        {
            var cart = await _uniteOfWork.CartRepository.GetCartWithItemsAsync(cartId);
            if (cart == null) return null;

            return CartDTOsMappers.CartDtoMapper(cart);
        }

    

        public async Task AddToCartAsync(int customerId, int productId, int quantity)
        {
            var cart = await _uniteOfWork.CartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new global::Buyonic.DAL.Cart { customerId = customerId };
                _uniteOfWork.CartRepository.Add(cart);
                await _uniteOfWork.SaveAsync();
            }

            var existingItem = await _uniteOfWork.CartRepository.GetCartItemAsync(cart.Id, productId);
            if (existingItem != null)
            {
                existingItem.quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new global::Buyonic.DAL.CartItem
                {
                    
                    productId = productId,
                    quantity = quantity
                });
            }

            await _uniteOfWork.SaveAsync();
        }

        public async Task UpdateCartItemAsync(int cartId, int productId, int quantity)
        {
            var cartItem = await _uniteOfWork.CartRepository.GetCartItemAsync(cartId, productId);
            if (cartItem == null) return;

            cartItem.quantity = quantity;
            await _uniteOfWork.SaveAsync();
        }


        //public async Task RemoveFromCartAsync(int cartId, int productId)
        //{
        //    var cart = await _uniteOfWork.CartRepository.GetCartWithItemsAsync(cartId);
        //    if (cart == null) return;

        //    var cartItem = cart.CartItems.FirstOrDefault(i => i.productId == productId);
        //    if (cartItem == null) return;

        //    cart.CartItems.Remove(cartItem);
        //    await _uniteOfWork.SaveAsync();
        //}

        //public async Task ClearCartAsync(int cartId)
        //{
        //    var cart = await _uniteOfWork.CartRepository.GetCartWithItemsAsync(cartId);
        //    if (cart == null) return;

        //    cart.CartItems.Clear();
        //    await _uniteOfWork.SaveAsync();
        //}


        public async Task RemoveFromCartAsync(int cartId, int productId)
        {
            var cartItem = await _uniteOfWork.CartRepository.GetCartItemAsync(cartId, productId);
            if (cartItem == null) return;

            _uniteOfWork.CartRepository.DeleteCartItem(cartItem); // ✅
            await _uniteOfWork.SaveAsync();
        }

        public async Task ClearCartAsync(int cartId)
        {
            var cart = await _uniteOfWork.CartRepository.GetCartWithItemsAsync(cartId);
            if (cart == null) return;

            foreach (var item in cart.CartItems.ToList())
                _uniteOfWork.CartRepository.DeleteCartItem(item); // ✅

            await _uniteOfWork.SaveAsync();
        }
    }
}


