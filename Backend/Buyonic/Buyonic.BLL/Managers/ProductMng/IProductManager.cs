using Buyonic.DAL;

namespace Buyonic.BLL.Managers.ProductMng
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();

        Task<IEnumerable<ProductWithSellerDTO>> GetProductsWithSellersAsync();

        Task<IEnumerable<ProductWithCategoryDTO>> GetProductsWithCategoriesAsync();

        Task<ProductDTO?> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId);

        Task<IEnumerable<ProductDTO>> GetProductsBySellerAsync(int sellerId);

        //Task<ProductDTO> AddProductAsync(CreateProductDTO dto);
        Task<ProductDTO> AddProductAsync(CreateProductDTO dto, string sellerEmail);

        Task<bool> UpdateProductAsync(int id, UpdateProductDTO dto, string? userEmail = null, bool isAdmin = false);

        Task<bool> DeleteProductAsync(int id);
    }
}