using Buyonic.BLL.Mappers;
using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryManager(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return categories.Select(CategoryDTOsMappers.CategoryDtoMapper);
        }

        public async Task<IEnumerable<CategoryWithProductsDTO>> GetCategoriesWithProductsAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesWithProductsAsync();
            return categories.Select(CategoryDTOsMappers.CategoryWithProductsDtoMapper);
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return null;
            return CategoryDTOsMappers.CategoryDtoMapper(category);
        }

        public async Task<CategoryWithProductsDTO?> GetCategoryByIdWithProductsAsync(int id)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesWithProductsAsync();
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return null;
            return CategoryDTOsMappers.CategoryWithProductsDtoMapper(category);
        }

        public async Task<CategoryDTO?> GetCategoryByNameAsync(string name)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByNameAsync(name);
            if (category == null) return null;
            return CategoryDTOsMappers.CategoryDtoMapper(category);
        }
        public async Task AddCategoryAsync(CategoryDTO dto)
        {
            var category = new Category
            {
                name = dto.Name,
                description = dto.Description
            };
            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(int id, CategoryDTO dto)
        {

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return;

            category.name = dto.Name;
            category.description = dto.Description;

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return;
            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveAsync();
        }
    }
}
