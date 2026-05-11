namespace Buyonic.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsWithSellersAsync();

        Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync();

        Task<IEnumerable<Product>> GetAllProductsWithOrderItemsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId);
    }
}