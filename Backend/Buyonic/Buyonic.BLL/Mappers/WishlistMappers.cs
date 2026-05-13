using Buyonic.DAL;

namespace Buyonic.BLL
{
    public static class WishlistMappers
    {
        public static WishlistDTO ToWishlistDTO(Wishlist w) => new WishlistDTO
        {
            Id = w.Id,
            CustomerId = w.customerId,
            Items = w.WishlistItems.Select(ToWishlistItemDTO)
        };

        public static WishlistItemDTO ToWishlistItemDTO(WishlistItem i) => new WishlistItemDTO
        {
            Id = i.Id,
            ProductId = i.productId,
            ProductName = i.Product.name,
            Price = i.Product.price,
            Rating = i.Product.rating,
            Discount = i.Product.discount
        };
    }
}