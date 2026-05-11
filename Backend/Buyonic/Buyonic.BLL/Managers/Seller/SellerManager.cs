using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class SellerManager : ISellerManager
    {
        private readonly IUnitOfWork _uniteOfWork;
        public SellerManager(IUnitOfWork unitOfWork) => _uniteOfWork = unitOfWork;

        public async Task<IEnumerable<SellerDTO>> GetSellersAsync()
        {
            var sellers = await _uniteOfWork.SellerRepository.GetAllAsync();
            return sellers.Select(Map);
        }

        public async Task<SellerDTO?> GetSellerByIdAsync(int id)
        {
            var seller = await _uniteOfWork.SellerRepository.GetSellerByIdAsync(id);
            if (seller == null) return null;
            return Map(seller);
        }

        public async Task<SellerDTO?> GetSellerByEmailAsync(string email)
        {
            var seller = await _uniteOfWork.SellerRepository.GetSellerByEmailAsync(email);
            if (seller == null) return null;
            return Map(seller);
        }

        public async Task<SellerWithProductsDTO?> GetSellerWithProductsAsync(int id)
        {
            var sellers = await _uniteOfWork.SellerRepository.GetAllSellersWithProductsAsync();
            var seller = sellers.FirstOrDefault(s => s.Id == id);
            if (seller == null) return null;
            return new SellerWithProductsDTO
            {
                Id = seller.Id,
                StoreName = seller.storeName,
                Rating = seller.rating,
                Products = seller.Products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.name,
                    Price = p.price,
                    Discount = p.discount,
                    Rating = p.rating,
                    StockQuantity = p.stockQuantity
                })
            };
        }

        private SellerDTO Map(Seller s) => new SellerDTO
        {
            Id = s.Id,
            StoreName = s.storeName,
            Rating = s.rating,
            FirstName = s.User.firstName,
            LastName = s.User.lastName,
            Email = s.User.Email!
        };
    }
}