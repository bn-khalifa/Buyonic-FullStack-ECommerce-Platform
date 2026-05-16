using Buyonic.BLL.DTOs.Payment;
using Buyonic.DAL;
using Buyonic.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.Mappers
{
    public class PaymentDTOsMappers
    {
        public static PaymentMethodDTO PaymentMethodDtoMapper(PaymentMethod p) => new PaymentMethodDTO
        {
            Id = p.Id,
            MethodName = p.methodName
        };

        public static CustomerPaymentDTO CustomerPaymentDtoMapper(CustomerPayment cp) => new CustomerPaymentDTO
        {
            Id = cp.Id,
            CustomerId = cp.customerId,
            PaymentMethodId = cp.paymentMethodId,
            MethodName = cp.PaymentMethod.methodName
        };
    }
}
