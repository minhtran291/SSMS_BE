using Microsoft.EntityFrameworkCore;
using SSMS.Domain.ExtendedEntities;
using SSMS.Application.Repositories.Base;
using SSMS.Infrastructure.DatabaseConfig;
using System.Linq.Expressions;

namespace SSMS.Infrastructure.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly SSMSContext _context;
    private DbSet<TEntity>? _entities;

    public Repository(SSMSContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected virtual DbSet<TEntity> Entities => _entities ??= _context.Set<TEntity>();

    public IQueryable<TEntity> Table => Entities;

    protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, bool includeDeleted)
    {
        if (includeDeleted)
            return query;

        // chi lay nhung cai ko xoa
        if (typeof(ISoftDeletedEntity).IsAssignableFrom(typeof(TEntity)))
            query = query.Where(e => !((ISoftDeletedEntity)e).IsDeleted);

        return query;
    }

    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual void Insert(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Entities.Add(entity);
    }

    public virtual async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entities);
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Insert(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Entities.AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Entities.Update(entity);
    }

    public virtual void Update(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Entities.UpdateRange(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        switch (entity)
        {
            case ISoftDeletedEntity softDeletedEntity:
                softDeletedEntity.IsDeleted = true;
                Entities.Update(entity);
                break;

            default:
                Entities.Remove(entity);
                break;
        }
    }

    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
            return;

        var softDeleteList = new List<TEntity>();
        var hardDeleteList = new List<TEntity>();

        foreach (var entity in entities)
        {
            if (entity is ISoftDeletedEntity soft)
            {
                soft.IsDeleted = true;
                softDeleteList.Add(entity);
            }
            else
                hardDeleteList.Add(entity);
        }
        if (softDeleteList.Any())
            Entities.UpdateRange(softDeleteList);

        if (hardDeleteList.Any())
            Entities.RemoveRange(hardDeleteList);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted, CancellationToken cancellationToken)
    {
        var query = AddDeletedFilter(Table, includeDeleted);

        return query.AnyAsync(predicate, cancellationToken);
    }

    public Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder, bool includeDeleted, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = AddDeletedFilter(Table, includeDeleted);

        query = query.AsNoTracking();

        return queryBuilder(query).ToListAsync(cancellationToken);
    }
}