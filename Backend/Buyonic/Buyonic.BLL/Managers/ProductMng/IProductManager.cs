using Buyonic.BLL.DTOs.Productdto;
using Buyonic.DAL; // ← دي بس كفاية

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

        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}