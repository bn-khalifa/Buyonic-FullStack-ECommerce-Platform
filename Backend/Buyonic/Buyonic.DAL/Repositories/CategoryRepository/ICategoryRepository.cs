namespace Buyonic.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesWithProductsAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        Task<Category> GetCategoryByNameAsync(string name);

       
    }
}