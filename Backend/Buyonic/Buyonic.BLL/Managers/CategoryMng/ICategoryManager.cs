using Buyonic.DAL;

namespace Buyonic.BLL
{
    public interface ICategoryManager
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<IEnumerable<CategoryWithProductsDTO>> GetCategoriesWithProductsAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryWithProductsDTO?> GetCategoryByIdWithProductsAsync(int id);
        Task<CategoryDTO?> GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
