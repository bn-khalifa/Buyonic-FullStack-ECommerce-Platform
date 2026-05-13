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

        Task<ProductDTO> AddProductAsync(CreateProductDTO dto);

        Task<bool> UpdateProductAsync(int id, UpdateProductDTO dto);

        Task<bool> DeleteProductAsync(int id);
    }
}