using Buyonic.DAL.Repositories.CartRepository;
using Buyonic.DAL.Repositories.OrderRepository;
using Buyonic.DAL.Repositories.PaymentMethodRepository;

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
        //public IPaymentMethodRepository PaymentMethodRepository { get; }

        public Task SaveAsync();
    }
}



