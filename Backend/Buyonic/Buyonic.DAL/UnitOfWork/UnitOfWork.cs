namespace Buyonic.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuyonicContext _context;
        public ICustomerRepository CustomerRepository { get; }
        public UnitOfWork(BuyonicContext context, ICustomerRepository customerReopsitory) {
            _context = context;
            CustomerRepository = customerReopsitory;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
