using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _uniteOfWork;
        public CustomerManager(IUnitOfWork unitOfWork)
        {
            _uniteOfWork = unitOfWork;
        }

        public async Task<CustomerDTO> GetCustomerByEmailAsync(string email)
        {
            var customer = await _uniteOfWork.CustomerRepository.GetCustomerByEmailAsync(email);
            if (customer == null) return null;
            return CustomerDTOsMappers.CustomerDtoMapper(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _uniteOfWork.CustomerRepository.GetAllAsync();
            return customers.Select(CustomerDTOsMappers.CustomerDtoMapper);
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _uniteOfWork.CustomerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;
            return CustomerDTOsMappers.CustomerDtoMapper(customer);
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            _uniteOfWork.CustomerRepository.Add(customer);
            await _uniteOfWork.SaveAsync();
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            bool result = await _uniteOfWork.CustomerRepository.DeleteCustomerByID(id);
            return result;
        }

        public async Task UpdateCustomerAsync(CustomerDTO c)
        {
            var customer = await _uniteOfWork.CustomerRepository.GetCustomerByIdAsync(c.Id);

            customer.User.firstName = c.FirstName;
            customer.User.lastName = c.LastName;
            customer.User.Email = c.Email;
            customer.address = c.Address;
            customer.User.isActive = c.IsActive;
            customer.User.updatedAt = DateTime.UtcNow;

            _uniteOfWork.CustomerRepository.Update(customer);
            await _uniteOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CustomerWithOrdersDTO>> GetAllCustomersWithOrdersAsync()
        {
            var customers = await _uniteOfWork.CustomerRepository.GetAllCustomersWithOrdersAsync();
            return customers.Select(CustomerDTOsMappers.CustomerWithOrdersDtoMapper);
        }
    }
}
