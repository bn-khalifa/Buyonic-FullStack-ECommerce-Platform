namespace Buyonic.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuyonicContext _context;
        public ICustomerRepository CustomerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public UnitOfWork(BuyonicContext context, 
                          ICustomerRepository customerReopsitory,
                          ISellerRepository sellerRepository) 
        {
            _context = context;
            CustomerRepository = customerReopsitory;
            SellerRepository = sellerRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
