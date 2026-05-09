namespace Buyonic.DAL
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public Task SaveAsync();
    }
}
