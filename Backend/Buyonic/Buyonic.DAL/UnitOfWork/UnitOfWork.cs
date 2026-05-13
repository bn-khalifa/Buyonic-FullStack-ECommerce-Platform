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

        public IReviewRepository ReviewRepository { get; }

        public UnitOfWork(
            BuyonicContext context,
            ICustomerRepository customerRepository,
            ISellerRepository sellerRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IPaymentMethodRepository paymentMethodRepository,
            ICustomerPaymentRepository customerPaymentRepository,
            IReviewRepository reviewRepository
            )
        {
            _context = context;
            CustomerRepository = customerRepository;
            SellerRepository = sellerRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            this.CartRepository = cartRepository;
            this.OrderRepository = orderRepository;
            this.PaymentMethodRepository = paymentMethodRepository;
            this.CustomerPaymentRepository = customerPaymentRepository;
            this.ReviewRepository = reviewRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
