using Buyonic.BLL.Buyonic.BLL;
using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class CustomerDTOsMappers
    {
        public static CustomerDTO CustomerDtoMapper(Customer c) => new CustomerDTO
        {
            Id = c.Id,
            FirstName = c.User.firstName,
            LastName = c.User.lastName,
            Email = c.User.Email!,
            IsActive = c.User.isActive,
            Address = c.address,
            JoinedAt = c.User.createdAt
        };

        public static CustomerWithOrdersDTO CustomerWithOrdersDtoMapper(Customer c) => new CustomerWithOrdersDTO
        {
            Id = c.Id,
            FirstName = c.User.firstName,
            LastName = c.User.lastName,
            Email = c.User.Email!,
            Address = c.address,
            Orders = c.Orders.Select(OrderDTOsMappers.OrderDtoMapper)
        };
    }
}
