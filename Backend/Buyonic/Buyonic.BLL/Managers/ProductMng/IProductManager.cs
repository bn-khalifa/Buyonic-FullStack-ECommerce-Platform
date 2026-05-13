using Buyonic.DAL;

namespace Buyonic.BLL.Managers.ProductMng
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<IEnumerable<ProductWithSellerDTO>> GetProductsWithSellersAsync();
        Task<IEnumerable<ProductWithCategoryDTO>> GetProductsWithCategoriesAsync();
        Task<ProductWithSellerDTO?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductDTO>> GetProductsBySellerAsync(int sellerId);

        Task AddProductAsync(ProductDTO product);
        Task UpdateProductAsync(int id, ProductDTO product);
        Task DeleteProductAsync(int id);
    }
}