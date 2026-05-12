using Buyonic.DAL.Repositories.CartRepository;
using Buyonic.DAL.Repositories.CustomerPaymentRepository;
using Buyonic.DAL.Repositories.OrderRepository;
using Buyonic.DAL.Repositories.PaymentMethodRepository;

namespace Buyonic.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuyonicContext _context;
        public ICustomerRepository CustomerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }

        public ICartRepository CartRepository { get; }

        public IOrderRepository OrderRepository { get; }
       
        public ICustomerPaymentRepository CustomerPaymentRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public UnitOfWork(
            BuyonicContext context,
            ICustomerRepository customerRepository,
            ISellerRepository sellerRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ICartRepository CartRepository,
            IOrderRepository OrderRepository,
            IPaymentMethodRepository PaymentMethodRepository,
            ICustomerPaymentRepository customerPaymentRepository
            
            )
        {
            _context = context;
            CustomerRepository = customerRepository;
            SellerRepository = sellerRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            this.CartRepository = CartRepository;      
            this.OrderRepository = OrderRepository;
            this.PaymentMethodRepository = PaymentMethodRepository;
            this.CustomerPaymentRepository = customerPaymentRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
