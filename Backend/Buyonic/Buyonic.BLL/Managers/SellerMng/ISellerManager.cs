using Buyonic.DAL;

namespace Buyonic.BLL
{
    public interface ISellerManager
    {
        Task<IEnumerable<SellerDTO>> GetSellersAsync();

        Task<IEnumerable<SellerWithProductsDTO>> GetSellersWithProductsAsync();

        Task<SellerDTO?> GetSellerByIdAsync(int id);

        Task<SellerDTO?> GetSellerByEmailAsync(string email);

        Task<SellerWithProductsDTO?> GetSellerByIdWithProductsAsync(int id);

        Task<SellerWithProductsDTO?> GetSellerByEmailWithProductsAsync(string email);

        Task<SellerDTO> AddSellerAsync(CreateSellerDTO dto);

        Task<bool> UpdateSellerAsync(int id, UpdateSellerDTO dto);

        Task<bool> DeleteSellerAsync(int id);
    }
}