using SSMS.Application.Common;
using SSMS.Application.DTOs;
using SSMS.Application.Common.Enums;
using System.Linq.Expressions;

namespace SSMS.Application.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> Query(QueryTracking tracking = QueryTracking.Tracking);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Insert(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default);
        Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder, bool includeDeleted = false, CancellationToken cancellationToken = default);
        Task<PagedResult<TResult>> PageAsync<TResult>(IQueryable<TResult> query, BaseSearchDTO search, CancellationToken cancellationToken = default);
    }
}
