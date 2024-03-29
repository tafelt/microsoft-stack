﻿namespace Domain.Common.SeedWork;

public interface IRepository<T, TId>
  where T : class, IAggregateRoot
{
  Task<IEnumerable<T>> GetAllAsync();

  Task<T?> GetByIdAsync(TId id);

  Task<T> CreateAsync(T entity);

  Task<T?> UpdateAsync(T entity);

  Task<T?> DeleteAsync(TId id);
}
