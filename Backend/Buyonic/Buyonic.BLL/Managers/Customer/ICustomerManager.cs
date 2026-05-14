using Buyonic.DAL;
namespace Buyonic.BLL
{
    public interface ICustomerManager
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<IEnumerable<CustomerWithOrdersDTO>> GetAllCustomersWithOrdersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
        Task<CustomerDTO> GetCustomerByEmailAsync(string email);
        Task<CustomerDTO?> GetCustomerByUserIdAsync(int userId);
        Task InsertCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task UpdateCustomerAsync(CustomerDTO c);
    }
}
