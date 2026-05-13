using Buyonic.DAL;

namespace Buyonic.BLL.Managers.ProductMng
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            return products.Select(ProductDTOsMappers.ProductDtoMapper);
        }

        public async Task<IEnumerable<ProductWithSellerDTO>> GetProductsWithSellersAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllProductsWithSellersAsync();

            return products.Select(ProductDTOsMappers.ProductWithSellerDtoMapper);
        }

        public async Task<IEnumerable<ProductWithCategoryDTO>> GetProductsWithCategoriesAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllProductsWithCategoriesAsync();

            return products.Select(ProductDTOsMappers.ProductWithCategoryDtoMapper);
        }

        public async Task<ProductWithSellerDTO?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);

            if (product == null)
                return null;

            return ProductDTOsMappers.ProductWithSellerDtoMapper(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsByCategoryAsync(categoryId);

            return products.Select(ProductDTOsMappers.ProductDtoMapper);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsBySellerAsync(int sellerId)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsBySellerAsync(sellerId);

            return products.Select(ProductDTOsMappers.ProductDtoMapper);
        }

        public async Task<ProductDTO> AddProductAsync(CreateProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                Price = dto.Price,
                Discount = dto.Discount,
                StockQuantity = dto.StockQuantity,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                SellerId = dto.SellerId
            };

            _unitOfWork.ProductRepository.Add(product);

            await _unitOfWork.SaveAsync();

            return ProductDTOsMappers.ProductDtoMapper(product);
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDTO dto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null || product.isDeleted)
                return false;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                product.Name = dto.Name;

            if (dto.ImageUrl != null)
                product.ImageUrl = dto.ImageUrl;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                product.Description = dto.Description;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;

            if (dto.Discount.HasValue)
                product.Discount = dto.Discount.Value;

            if (dto.StockQuantity.HasValue)
                product.StockQuantity = dto.StockQuantity.Value;

            if (dto.CategoryId.HasValue)
                product.CategoryId = dto.CategoryId.Value;

            if (dto.SellerId.HasValue)
                product.SellerId = dto.SellerId.Value;

            product.updatedAt = DateTime.UtcNow;

            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null || product.isDeleted)
                return false;

            product.isDeleted = true;

            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.SaveAsync();

            return true;
        }

       
    }
}