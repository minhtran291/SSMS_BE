using System.Linq.Expressions;

namespace SSMS.Domain.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T?> GetByIdAsync(object id);
        //Task AddAsync(T entity, CancellationToken cancellationToken = default);
        //Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        //void Update(T entity);
        //void UpdateRange(IEnumerable<T> entities);
        //void Remove(T entity);
        //void RemoveRange(IEnumerable<T> entities);
        //IQueryable<T> Query();
        //Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        //Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        IQueryable<T> Table { get; }

        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        void Insert(T entity);
        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Insert(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
    }
}
