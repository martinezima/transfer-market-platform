namespace Domain.Interfaces;

public interface IRepository<TEntity, TKey>
    where TEntity : class
{
    Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetQuery();
    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
    Task<int> BulkDeleteByIds(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default,
        string keyName = "Id"
    );
}
