using Buyonic.BLL.DTOs.Categorydto;
using Buyonic.DAL;

namespace Buyonic.BLL.Mappers
{
    public class CategoryDTOsMappers
    {
        public static CategoryDTO CategoryDtoMapper(Category c) => new CategoryDTO
        {
            Id = c.Id,
            Name = c.name,
            Description = c.description
        };

        public static CategoryWithProductsDTO CategoryWithProductsDtoMapper(Category c) => new CategoryWithProductsDTO
        {
            Id = c.Id,
            Name = c.name,
            Description = c.description,
            Products = c.products.Select(ProductDTOsMappers.ProductDtoMapper)
        };
    }
}
