using Buyonic.DAL;

namespace Buyonic.BLL.Managers.ProductMng
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManager(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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
            if (product == null) return null;
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


        public async Task AddProductAsync(ProductDTO dto)
        {

            var product = new Product
            {
                name = dto.Name,
                imageUrl = dto.ImageUrl,
                price = dto.Price,
                discount = dto.Discount,
                stockQuantity = dto.StockQuantity,
                description = dto.Description,
                categoryId = dto.CategoryId,
                sellerId = dto.SellerId
            };
            _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProductAsync(int id,ProductDTO dto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return;

            product.name = dto.Name;
            product.imageUrl = dto.ImageUrl;
            product.price = dto.Price;
            product.discount = dto.Discount;
            product.stockQuantity = dto.StockQuantity;
            product.description = dto.Description;
            product.sellerId = dto.SellerId;
            product.categoryId = dto.CategoryId;

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return;
            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
