using Application.Abstractions;
using Application.Repositories;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
  private readonly ISqlConnectionFactory _sqlConnectionFactory;

  public UserRepository(ISqlConnectionFactory sqlConnectionFactory)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
  }

  public Task<List<User>> GetAllAsync()
  {
    using SqlConnection connection = _sqlConnectionFactory.CreateConnection();

    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [User];
      ";

    var result = connection.QueryAsync<User>(Sql).GetAwaiter().GetResult().ToList();

    return Task.FromResult(result);
  }

  public Task<User> GetByIdAsync(long id)
  {
    using SqlConnection connection = _sqlConnectionFactory.CreateConnection();

    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [User]
        WHERE [Id] = @Id;
      ";

    var result = connection.QuerySingleAsync<User>(Sql, new { Id = id }).GetAwaiter().GetResult();

    return Task.FromResult(result);
  }

  public Task<User> CreateAsync(User entity)
  {
    using SqlConnection connection = _sqlConnectionFactory.CreateConnection();

    const string Sql =
      @"
        INSERT INTO [User] ([Name], [Email])
        OUTPUT inserted.Id, inserted.Name, inserted.Email
        VALUES (@Name, @Email);
      ";

    var result = connection
      .QuerySingleAsync<User>(Sql, new { entity.Name, entity.Email })
      .GetAwaiter()
      .GetResult();

    return Task.FromResult(result);
  }

  public Task<User> UpdateAsync(User entity)
  {
    using SqlConnection connection = _sqlConnectionFactory.CreateConnection();

    const string Sql =
      @"
        UPDATE [User]
        SET [Name] = @Name,
            [Email] = @Email
        OUTPUT inserted.Id, inserted.Name, inserted.Email
        WHERE [Id] = @Id;
      ";

    var result = connection
      .QuerySingleAsync<User>(
        Sql,
        new
        {
          entity.Id,
          entity.Name,
          entity.Email
        }
      )
      .GetAwaiter()
      .GetResult();

    return Task.FromResult(result);
  }

  public Task<User> DeleteAsync(long id)
  {
    using SqlConnection connection = _sqlConnectionFactory.CreateConnection();

    const string Sql =
      @"
        DELETE FROM [User]
        OUTPUT deleted.Id, deleted.Name, deleted.Email
        WHERE [Id] = @Id;
      ";

    var result = connection.QuerySingleAsync<User>(Sql, new { Id = id }).GetAwaiter().GetResult();

    return Task.FromResult(result);
  }
}
