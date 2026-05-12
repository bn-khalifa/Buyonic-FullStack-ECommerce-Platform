using Buyonic.BLL.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.Managers.Payment
{
    public interface IPaymentManager
    {
        Task<IEnumerable<PaymentMethodDTO>> GetAllPaymentMethodsAsync();
        Task<PaymentMethodDTO> GetPaymentMethodByIdAsync(int id);
        Task<IEnumerable<CustomerPaymentDTO>> GetCustomerPaymentsAsync(int customerId);
        Task<CustomerPaymentDTO> AddCustomerPaymentAsync(AddCustomerPaymentDTO dto);
        Task<bool> RemoveCustomerPaymentAsync(int customerPaymentId);
    }
}
