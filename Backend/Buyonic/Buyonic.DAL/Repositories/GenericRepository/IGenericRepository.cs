using System.Linq.Expressions;

namespace Buyonic.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
