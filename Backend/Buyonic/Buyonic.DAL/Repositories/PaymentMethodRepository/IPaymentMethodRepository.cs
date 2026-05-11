using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Repositories.PaymentMethodRepository
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();

        Task<PaymentMethod> GetPaymentMethodByIdAsync(int id);
    }
}
