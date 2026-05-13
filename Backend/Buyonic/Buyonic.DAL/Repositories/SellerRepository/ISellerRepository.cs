namespace Buyonic.DAL
{
    public interface ISellerRepository : IGenericRepository<Seller>
    {
        Task<IEnumerable<Seller>> GetAllSellersAsync();

        Task<IEnumerable<Seller>> GetAllSellersWithProductsAsync();

        Task<Seller?> GetSellerByIdAsync(int id);

        Task<Seller?> GetSellerByEmailAsync(string email);

        Task<Seller?> GetSellerWithProductsByIdAsync(int id);

        Task<Seller?> GetSellerWithProductsByEmailAsync(string email);

        Task<Seller?> GetSellerByStoreNameAsync(string storeName);
    }
}