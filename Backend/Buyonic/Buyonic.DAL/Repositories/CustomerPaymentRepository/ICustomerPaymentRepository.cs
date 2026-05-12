using Buyonic.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Repositories.CustomerPaymentRepository
{
    public interface ICustomerPaymentRepository : IGenericRepository<CustomerPayment>
    {
        Task<IEnumerable<CustomerPayment>> GetCustomerPaymentsAsync(int customerId);
        Task<CustomerPayment> GetCustomerPaymentByIdAsync(int customerPaymentId);
    }
}
