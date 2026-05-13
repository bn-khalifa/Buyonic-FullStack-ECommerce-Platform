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
        public IWishlistRepository WishlistRepository { get; }

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
            IReviewRepository reviewRepository,
            IWishlistRepository wishlistRepository
            )
        {
            _context = context;
            CustomerRepository = customerRepository;
            SellerRepository = sellerRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            CartRepository = cartRepository;
            OrderRepository = orderRepository;
            PaymentMethodRepository = paymentMethodRepository;
            CustomerPaymentRepository = customerPaymentRepository;
            ReviewRepository = reviewRepository;
            WishlistRepository = wishlistRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
