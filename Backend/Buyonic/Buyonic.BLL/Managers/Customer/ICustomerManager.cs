using Buyonic.BLL.Buyonic.BLL;
using Buyonic.DAL;
namespace Buyonic.BLL
{
    public interface ICustomerManager
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
        public Task<CustomerDTO> GetCustomerByEmailAsync(string email);
        public Task InsertCustomerAsync(Customer customer);
    }
}
