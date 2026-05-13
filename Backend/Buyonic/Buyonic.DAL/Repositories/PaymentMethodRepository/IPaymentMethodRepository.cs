namespace Buyonic.DAL
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();

        Task<PaymentMethod> GetPaymentMethodByIdAsync(int id);
    }
}
