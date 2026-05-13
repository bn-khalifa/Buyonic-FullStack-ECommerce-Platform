using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class PaymentMethodRepository :
          GenericRepository<PaymentMethod>,
          IPaymentMethodRepository
    {
        public PaymentMethodRepository(BuyonicContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int id)
        {
            return await _context.PaymentMethods
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
