using Buyonic.BLL.Buyonic.BLL;
using Buyonic.DAL;

namespace Buyonic.BLL
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _uniteOfWork;
        public CustomerManager(IUnitOfWork unitOfWork) { 
            _uniteOfWork = unitOfWork;
        }

        public async Task<CustomerDTO> GetCustomerByEmailAsync(string email)
        {
            var customer = await _uniteOfWork.CustomerRepository.GetCustomerByEmailAsync(email);
            if (customer == null) return null;
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.User.firstName,
                LastName = customer.User.lastName,
                Email = customer.User.Email,
                Address = customer.address,
                JoinedAt = customer.User.createdAt,
                IsActive = customer.User.isActive
            };
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _uniteOfWork.CustomerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FirstName = c.User.firstName,
                LastName = c.User.lastName,
                Email = c.User.Email,
                Address = c.address,
                JoinedAt = c.User.createdAt,
                IsActive = c.User.isActive
            });
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _uniteOfWork.CustomerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.User.firstName,
                LastName = customer.User.lastName,
                Email = customer.User.Email,
                Address = customer.address,
                JoinedAt = customer.User.createdAt,
                IsActive = customer.User.isActive
            };
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            _uniteOfWork.CustomerRepository.Add(customer);
            await _uniteOfWork.SaveAsync();
        }
    }
}
