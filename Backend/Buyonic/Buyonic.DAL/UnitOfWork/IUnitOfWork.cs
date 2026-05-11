namespace Buyonic.DAL
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public Task SaveAsync();
    }
}



