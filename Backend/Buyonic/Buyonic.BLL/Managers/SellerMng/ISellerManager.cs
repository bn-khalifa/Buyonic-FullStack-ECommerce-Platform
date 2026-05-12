using Buyonic.BLL.DTOs.Sellerdto;
using Buyonic.DAL;

namespace Buyonic.BLL.Managers.SellerMng
{
    public interface ISellerManager
    {
        // GET
        Task<IEnumerable<SellerDTO>> GetSellersAsync();
        Task<IEnumerable<SellerWithProductsDTO>> GetSellersWithProductsAsync();
        Task<SellerDTO?> GetSellerByIdAsync(int id);
        Task<SellerDTO?> GetSellerByEmailAsync(string email);
        Task<SellerWithProductsDTO?> GetSellerByIdWithProductsAsync(int id);
        Task<SellerWithProductsDTO?> GetSellerByEmailWithProductsAsync(string email);

        

        // POST - PUT - DELETE
        Task AddSellerAsync(Seller seller);
        Task UpdateSellerAsync(Seller seller);
        Task DeleteSellerAsync(int id);
    }
}