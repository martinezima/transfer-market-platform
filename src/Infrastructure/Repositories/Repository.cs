using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly TransferMarketDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(TransferMarketDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default
    )
    {
        return await DbSet.FindAsync(new object?[] { id }, cancellationToken);
    }

    public virtual IQueryable<TEntity> GetQuery()
    {
        return DbSet.AsQueryable();
    }

    public virtual async Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(
        TKey id,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
