using Buyonic.BLL.DTOs.Payment;
using Buyonic.BLL.Mappers;
using Buyonic.DAL;
using Buyonic.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.BLL.Managers.Payment
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IUnitOfWork _uniteOfWork;

        public PaymentManager(IUnitOfWork unitOfWork)
        {
            _uniteOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentMethodDTO>> GetAllPaymentMethodsAsync()
        {
            var methods = await _uniteOfWork.PaymentMethodRepository.GetAllPaymentMethodsAsync();
            return methods.Select(PaymentDTOsMappers.PaymentMethodDtoMapper);
        }

        public async Task<PaymentMethodDTO> GetPaymentMethodByIdAsync(int id)
        {
            var method = await _uniteOfWork.PaymentMethodRepository.GetPaymentMethodByIdAsync(id);
            if (method == null) return null;

            return PaymentDTOsMappers.PaymentMethodDtoMapper(method);
        }

        public async Task<IEnumerable<CustomerPaymentDTO>> GetCustomerPaymentsAsync(int customerId)
        {
            var payments = await _uniteOfWork.CustomerPaymentRepository.GetCustomerPaymentsAsync(customerId);
            return payments.Select(PaymentDTOsMappers.CustomerPaymentDtoMapper);
        }

        public async Task<CustomerPaymentDTO> AddCustomerPaymentAsync(AddCustomerPaymentDTO dto)
        {
            // تأكد إن الـ PaymentMethod موجود
            var method = await _uniteOfWork.PaymentMethodRepository.GetPaymentMethodByIdAsync(dto.PaymentMethodId);
            if (method == null)
                throw new InvalidOperationException("Payment method not found.");

            var customerPayment = new CustomerPayment
            {
                customerId = dto.CustomerId,
                paymentMethodId = dto.PaymentMethodId
            };

            _uniteOfWork.CustomerPaymentRepository.Add(customerPayment);
            await _uniteOfWork.SaveAsync();

            var created = await _uniteOfWork.CustomerPaymentRepository.GetCustomerPaymentByIdAsync(customerPayment.Id);
            return PaymentDTOsMappers.CustomerPaymentDtoMapper(created);
        }

        public async Task<bool> RemoveCustomerPaymentAsync(int customerPaymentId)
        {
            var customerPayment = await _uniteOfWork.CustomerPaymentRepository.GetCustomerPaymentByIdAsync(customerPaymentId);
            if (customerPayment == null) return false;

            _uniteOfWork.CustomerPaymentRepository.Delete(customerPayment);
            await _uniteOfWork.SaveAsync();
            return true;
        }
    }
}
