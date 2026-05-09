namespace Buyonic.DAL
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersWithOrdersAsync();
        Task<IEnumerable<Customer>> GetAllCustomersWithCartsAsync();
        Task<IEnumerable<Customer>> GetAllCustomersWithWishlistsAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}
