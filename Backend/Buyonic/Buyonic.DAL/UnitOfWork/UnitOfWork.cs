namespace Buyonic.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuyonicContext _context;
        public ICustomerRepository CustomerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }

        public UnitOfWork(
            BuyonicContext context,
            ICustomerRepository customerRepository,
            ISellerRepository sellerRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _context = context;
            CustomerRepository = customerRepository;
            SellerRepository = sellerRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
