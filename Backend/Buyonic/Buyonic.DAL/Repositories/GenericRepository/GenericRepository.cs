using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BuyonicContext _context;
        public GenericRepository(BuyonicContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
