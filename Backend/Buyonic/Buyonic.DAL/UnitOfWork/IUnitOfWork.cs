namespace Buyonic.DAL
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public Task SaveAsync();
    }
}
