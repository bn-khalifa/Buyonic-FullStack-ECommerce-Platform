using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class WishlistManager : IWishlistManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishlistManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WishlistDTO?> GetWishlistAsync(int customerId)
        {
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(customerId);
            if (wishlist == null) return null;
            return WishlistMappers.ToWishlistDTO(wishlist);
        }

        public async Task<bool> AddToWishlistAsync(int customerId, int productId)
        {
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(customerId);
            if (wishlist == null)
            {
                _unitOfWork.WishlistRepository.Add(new Wishlist
                {
                    customerId = customerId,
                    WishlistItems = new List<WishlistItem>()
                });
                await _unitOfWork.SaveAsync(); // save first
                wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(customerId);
            }

            // prevent duplicates
            var existing = await _unitOfWork.WishlistRepository.GetWishlistItemAsync(wishlist.Id, productId);
            if (existing != null) return false;

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null) return false;
            
            _unitOfWork.WishlistRepository.AddItemAsync(new WishlistItem
            {
                wishlistId = wishlist.Id,
                productId = productId
            });

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int customerId, int productId)
        {
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(customerId);
            if (wishlist == null) return false;

            var item = await _unitOfWork.WishlistRepository.GetWishlistItemAsync(wishlist.Id, productId);
            if (item == null) return false;

            await _unitOfWork.WishlistRepository.RemoveItemAsync(item);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> ClearWishlistAsync(int customerId)
        {
            var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByCustomerIdAsync(customerId);
            if (wishlist == null) return false;

            await _unitOfWork.WishlistRepository.ClearWishlistAsync(wishlist.Id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}