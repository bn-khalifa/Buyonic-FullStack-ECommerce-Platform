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
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return null;

            return CategoryDTOsMappers.CategoryWithProductsDtoMapper(category);
        }

        public async Task<CategoryDTO?> GetCategoryByNameAsync(string name)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByNameAsync(name);
            if (category == null) return null;
            return CategoryDTOsMappers.CategoryDtoMapper(category);
        }
        public async Task<CategoryDTO> AddCategoryAsync(CreateCategoryDTO dto)
        {
            var existing = await _unitOfWork.CategoryRepository.GetCategoryByNameAsync(dto.Name);
            if (existing != null)
                throw new InvalidOperationException($"A category named '{dto.Name}' already exists.");

            var category = new Category
            {
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim()
            };
            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveAsync();
            return CategoryDTOsMappers.CategoryDtoMapper(category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryDTO dto)
        {

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return;

            category.Name = dto.Name;
            category.Description = dto.Description;

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
