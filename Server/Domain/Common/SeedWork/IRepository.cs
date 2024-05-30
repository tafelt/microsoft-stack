namespace Domain.Common.SeedWork;

public interface IRepository<TEntity, TId>
  where TEntity : class, IAggregateRoot
  where TId : notnull
{
  Task<IEnumerable<TEntity>> GetAllAsync();

  Task<TEntity?> GetByIdAsync(TId id);

  Task<TEntity> CreateAsync(TEntity entity);

  Task<TEntity?> UpdateAsync(TEntity entity);

  Task<TEntity?> DeleteAsync(TId id);
}
