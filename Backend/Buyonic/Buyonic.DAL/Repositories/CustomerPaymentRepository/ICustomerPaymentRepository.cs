using Buyonic.DAL.Data.Models;

namespace Buyonic.DAL
{
    public interface ICustomerPaymentRepository : IGenericRepository<CustomerPayment>
    {
        Task<IEnumerable<CustomerPayment>> GetCustomerPaymentsAsync(int customerId);
        Task<CustomerPayment> GetCustomerPaymentByIdAsync(int customerPaymentId);
    }
}
