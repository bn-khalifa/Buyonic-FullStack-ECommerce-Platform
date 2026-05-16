using Buyonic.DAL;

namespace Buyonic.BLL.Mappers
{
    public class CategoryDTOsMappers
    {
        public static CategoryDTO CategoryDtoMapper(Category c) => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        };

        public static CategoryWithProductsDTO CategoryWithProductsDtoMapper(Category c)
        {
            return new CategoryWithProductsDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,

                Products = c.Products?
                    .Select(ProductDTOsMappers.ProductDtoMapper)
                    ?? new List<ProductDTO>()
            };
        }
    }
}
