using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly ISqlConnectionFactory _sqlConnectionFactory;

  public UserRepository(ISqlConnectionFactory sqlConnectionFactory)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
  }

  public Task<IEnumerable<User>> GetAllAsync()
  {
    using SqlConnection connection = _sqlConnectionFactory.GetOpenConnection();

    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [dbo].[User];
      ";

    var result = connection.QueryAsync<User>(Sql).GetAwaiter().GetResult();

    return Task.FromResult(result);
  }

  public Task<User?> GetByIdAsync(long id)
  {
    using SqlConnection connection = _sqlConnectionFactory.GetOpenConnection();

    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [dbo].[User]
        WHERE [Id] = @Id;
      ";

    var result = connection
      .QuerySingleOrDefaultAsync<User>(Sql, new { Id = id })
      .GetAwaiter()
      .GetResult();

    return Task.FromResult(result);
  }

  public Task<User> CreateAsync(User entity)
  {
    using SqlConnection connection = _sqlConnectionFactory.GetOpenConnection();

    const string Sql =
      @"
        INSERT INTO [dbo].[User] ([Name], [Email])
        OUTPUT inserted.Id, inserted.Name, inserted.Email
        VALUES (@Name, @Email);
      ";

    var result = connection
      .QuerySingleAsync<User>(Sql, new { entity.Name, entity.Email })
      .GetAwaiter()
      .GetResult();

    return Task.FromResult(result);
  }

  public Task<User?> UpdateAsync(User entity)
  {
    using SqlConnection connection = _sqlConnectionFactory.GetOpenConnection();

    const string Sql =
      @"
        UPDATE [dbo].[User]
        SET [Name] = @Name,
            [Email] = @Email
        OUTPUT inserted.Id, inserted.Name, inserted.Email
        WHERE [Id] = @Id;
      ";

    var result = connection
      .QuerySingleOrDefaultAsync<User>(
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

  public Task<User?> DeleteAsync(long id)
  {
    using SqlConnection connection = _sqlConnectionFactory.GetOpenConnection();

    const string Sql =
      @"
        DELETE FROM [dbo].[User]
        OUTPUT deleted.Id, deleted.Name, deleted.Email
        WHERE [Id] = @Id;
      ";

    var result = connection
      .QuerySingleOrDefaultAsync<User>(Sql, new { Id = id })
      .GetAwaiter()
      .GetResult();

    return Task.FromResult(result);
  }
}
