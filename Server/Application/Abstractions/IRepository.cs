﻿namespace Application.Repositories;

public interface IRepository<T>
  where T : class
{
  Task<List<T>> GetAllAsync();

  Task<T> GetByIdAsync(long id);

  Task<T> CreateAsync(T entity);

  Task<T> UpdateAsync(T entity);

  Task<T> DeleteAsync(long id);
}
