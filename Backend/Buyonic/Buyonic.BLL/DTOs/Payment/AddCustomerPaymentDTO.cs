using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.DTOs.Payment
{
    public class AddCustomerPaymentDTO
    {
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
