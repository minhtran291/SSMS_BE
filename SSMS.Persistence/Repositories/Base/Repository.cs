using Microsoft.EntityFrameworkCore;
using SSMS.Domain.ExtendedEntities;
using SSMS.Domain.Repositories.Base;
using SSMS.Infrustructure.DatabaseConfig;

namespace SSMS.Infrustructure.Repositories.Base;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly SSMSContext _context;
    private DbSet<T>? _entities;

    public Repository(SSMSContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected virtual DbSet<T> Entities => _entities ??= _context.Set<T>();

    public IQueryable<T> Table => Entities;

    public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual void Insert(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Entities.Add(entity);
    }

    public virtual async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Insert(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Entities.AddRange(entities);
    }

    public virtual void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Entities.Update(entity);
    }

    public virtual void Update(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Entities.UpdateRange(entities);
    }

    public virtual void Delete(T entity)
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

    public virtual void Delete(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
            return;

        var softDeleteList = new List<T>();
        var hardDeleteList = new List<T>();

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
}