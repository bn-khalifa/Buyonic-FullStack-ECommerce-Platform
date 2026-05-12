namespace Buyonic.DAL
{
    public interface ISellerRepository : IGenericRepository<Seller>
    {
        Task<IEnumerable<Seller>> GetAllSellersWithProductsAsync();

        Task<Seller> GetSellerByIdAsync(int id);
        Task<Seller> GetSellerByEmailAsync(string email);
        Task<Seller> GetSellerByStoreNameAsync(string storeName);

        Task<Seller> GetSellerByEmailAsync(string email);

        Task<IEnumerable<Seller>> GetAllSellersAsync();

    }
}