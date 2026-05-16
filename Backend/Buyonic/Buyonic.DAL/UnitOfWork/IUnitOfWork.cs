namespace Buyonic.DAL
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }

        public ICartRepository CartRepository { get; }

        public IOrderRepository OrderRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }

        public ICustomerPaymentRepository CustomerPaymentRepository { get; }

        public IReviewRepository ReviewRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        public Task SaveAsync();

        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}



