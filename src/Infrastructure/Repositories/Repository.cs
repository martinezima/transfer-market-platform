using System.Linq.Expressions;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly TransferMarketDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(TransferMarketDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetById(
        TKey id,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.FindAsync(new object?[] { id }, cancellationToken);
    }

    public virtual IQueryable<TEntity> GetQuery()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<TEntity> Create(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> Update(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetById(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    ///  Here dealing with an Entity Framework Core LINQ translation error!
    ///  Trying to use GetType() and GetProperty() inside the Where() clause
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public virtual async Task<int> BulkDeleteByIds_OLD(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default,
        string keyName = "Id"
    )
    {
        var entitiesToDelete = _dbSet.Where(e =>
            ids.Contains((TKey)e.GetType().GetProperty(keyName)!.GetValue(e)!)
        );

        _dbSet.RemoveRange(entitiesToDelete);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Use Expression Trees to build a dynamic LINQ query for filtering entities by their IDs.
    /// This approach avoids the EF Core LINQ translation error that occurs
    /// when using reflection methods like GetType() and GetProperty() inside the Where() clause.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<int> BulkDeleteByIds(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default,
        string keyName = "Id"
    )
    {
        var idList = ids.ToList();

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, keyName);
        var containsMethod =
            typeof(List<TKey>).GetMethod("Contains")
            ?? throw new InvalidOperationException("Contains method not found on List<TKey>");
        var containsCall = Expression.Call(Expression.Constant(idList), containsMethod, property);
        var predicate = Expression.Lambda<Func<TEntity, bool>>(containsCall, parameter);
        var entitiesToDelete = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        _dbSet.RemoveRange(entitiesToDelete);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
