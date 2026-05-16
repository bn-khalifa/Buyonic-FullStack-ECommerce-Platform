using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.DTOs.Payment
{
    public class CustomerPaymentDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
    }
}
