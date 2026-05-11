namespace Buyonic.BLL
{
    public interface ISellerManager
    {
        Task<IEnumerable<SellerDTO>> GetSellersAsync();
        Task<SellerWithProductsDTO?> GetSellerWithProductsAsync(int id);
        Task<SellerDTO?> GetSellerByIdAsync(int id);
        Task<SellerDTO?> GetSellerByEmailAsync(string email);
    }
}
