using Domain.Users;
using Infrastructure.Persistance.DataAccess.SqlServer;

namespace Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
  private readonly ISqlConnector _sqlConnector;

  public UserRepository(ISqlConnector sqlConnector)
  {
    _sqlConnector = sqlConnector;
  }

  public Task<IEnumerable<User>> GetAllAsync()
  {
    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [dbo].[User];
      ";

    return _sqlConnector.QueryAsync<User>(Sql);
  }

  public Task<User?> GetByIdAsync(int id)
  {
    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [dbo].[User]
        WHERE [Id] = @Id;
      ";

    return _sqlConnector.QuerySingleOrDefaultAsync<User>(Sql, new { Id = id });
  }

  public Task<User?> GetByEmailAsync(string email)
  {
    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email]
        FROM [dbo].[User]
        WHERE [Email] = @Email;
      ";

    return _sqlConnector.QuerySingleOrDefaultAsync<User>(Sql, new { Email = email });
  }

  public Task<User> CreateAsync(User entity)
  {
    const string Sql =
      @"
        INSERT INTO [dbo].[User] ([Name], [Email])
        OUTPUT inserted.Id, inserted.Name, inserted.Email
        VALUES (@Name, @Email);
      ";

    return _sqlConnector.QuerySingleAsync<User>(Sql, new { entity.Name, entity.Email });
  }

  public Task<User?> UpdateAsync(User entity)
  {
    const string Sql =
      @"
        UPDATE [dbo].[User]
        SET [Name] = @Name,
            [Email] = @Email
        OUTPUT inserted.Id, inserted.Name, inserted.Email
        WHERE [Id] = @Id;
      ";

    return _sqlConnector.QuerySingleOrDefaultAsync<User>(
      Sql,
      new
      {
        entity.Id,
        entity.Name,
        entity.Email
      }
    );
  }

  public Task<User?> DeleteAsync(int id)
  {
    const string Sql =
      @"
        DELETE FROM [dbo].[User]
        OUTPUT deleted.Id, deleted.Name, deleted.Email
        WHERE [Id] = @Id;
      ";

    return _sqlConnector.QuerySingleOrDefaultAsync<User>(Sql, new { Id = id });
  }
}
