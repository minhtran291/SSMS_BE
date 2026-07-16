using System.Linq.Expressions;

namespace SSMS.Domain.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Insert(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool includeDeleted = false);
    }
}
