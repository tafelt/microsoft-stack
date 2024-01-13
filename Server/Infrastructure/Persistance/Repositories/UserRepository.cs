using Dapper;
using Domain.Users;
using Infrastructure.Persistance.DataAccess.SqlServer;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistance.Repositories;

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

  public Task<User?> GetByIdAsync(int id)
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

  public Task<User?> GetByEmailAsync(string email)
  {
    using SqlConnection connection = _sqlConnectionFactory.GetOpenConnection();

    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [dbo].[User]
        WHERE [Email] = @Email;
      ";

    var result = connection
      .QuerySingleOrDefaultAsync<User>(Sql, new { Email = email })
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

  public Task<User?> DeleteAsync(int id)
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
