using Buyonic.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Repositories.CustomerPaymentRepository
{
    public class CustomerPaymentRepository : GenericRepository<CustomerPayment>, ICustomerPaymentRepository
    {
        public CustomerPaymentRepository(BuyonicContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CustomerPayment>> GetCustomerPaymentsAsync(int customerId)
        {
            return await _context.CustomerPayments
                .Include(cp => cp.PaymentMethod)
                .Where(cp => cp.customerId == customerId)
                .ToListAsync();
        }

        public async Task<CustomerPayment> GetCustomerPaymentByIdAsync(int customerPaymentId)
        {
            return await _context.CustomerPayments
                .Include(cp => cp.PaymentMethod)
                .FirstOrDefaultAsync(cp => cp.Id == customerPaymentId);
        }
    }
}
