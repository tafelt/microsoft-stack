using Dapper;
using Domain.Users;
using Infrastructure.DataAccess.SqlServer;

namespace Infrastructure.Repositories;

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
          [Email],
          [Street],
          [City],
          [State],
          [Country],
          [ZipCode]
        FROM [dbo].[User];
      ";

    return _sqlConnector.RunAsync(
      connection => connection.QueryAsync(Sql, MapData, splitOn: "Street")
    );
  }

  public Task<User?> GetByIdAsync(int id)
  {
    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email],
          [Street],
          [City],
          [State],
          [Country],
          [ZipCode]
        FROM [dbo].[User]
        WHERE [Id] = @Id;
      ";

    return _sqlConnector.RunAsync(async connection =>
    {
      var result = await connection.QueryAsync(Sql, MapData, new { Id = id }, splitOn: "Street");

      return result.FirstOrDefault();
    });
  }

  public Task<User?> GetByEmailAsync(string email)
  {
    const string Sql =
      @"
        SELECT
          [Id],
          [Name],
          [Email],
          [Street],
          [City],
          [State],
          [Country],
          [ZipCode]
        FROM [dbo].[User]
        WHERE [Email] = @Email;
      ";

    return _sqlConnector.RunAsync(async connection =>
    {
      var result = await connection.QueryAsync(
        Sql,
        MapData,
        new { Email = email },
        splitOn: "Street"
      );

      return result.FirstOrDefault();
    });
  }

  public Task<User> CreateAsync(User entity)
  {
    const string Sql =
      @"
        INSERT INTO [dbo].[User] ([Name], [Email], [Street], [City], [State], [Country], [ZipCode])
        OUTPUT inserted.Id, inserted.Name, inserted.Email, inserted.Street, inserted.City, inserted.State, inserted.Country, inserted.ZipCode
        VALUES (@Name, @Email, @Street, @City, @State, @Country, @ZipCode);
      ";

    return _sqlConnector.RunInsideTransactionAsync(
      async (connection, transaction) =>
      {
        var result = await connection.QueryAsync(
          Sql,
          MapData,
          new
          {
            entity.Name,
            entity.Email,
            entity.Address.Street,
            entity.Address.City,
            entity.Address.State,
            entity.Address.Country,
            entity.Address.ZipCode
          },
          transaction,
          splitOn: "Street"
        );

        return result.First();
      }
    );
  }

  public Task<User?> UpdateAsync(User entity)
  {
    const string Sql =
      @"
        UPDATE [dbo].[User]
        SET [Name] = @Name,
            [Street] = @Street,
            [City] = @City,
            [State] = @State,
            [Country] = @Country,
            [ZipCode] = @ZipCode
        OUTPUT inserted.Id, inserted.Name, inserted.Email, inserted.Street, inserted.City, inserted.State, inserted.Country, inserted.ZipCode
        WHERE [Id] = @Id;
      ";

    return _sqlConnector.RunInsideTransactionAsync(
      async (connection, transaction) =>
      {
        var result = await connection.QueryAsync(
          Sql,
          MapData,
          new
          {
            entity.Id,
            entity.Name,
            entity.Address.Street,
            entity.Address.City,
            entity.Address.State,
            entity.Address.Country,
            entity.Address.ZipCode
          },
          transaction,
          splitOn: "Street"
        );

        return result.FirstOrDefault();
      }
    );
  }

  public Task<User?> DeleteAsync(int id)
  {
    const string Sql =
      @"
        DELETE FROM [dbo].[User]
        OUTPUT deleted.Id, deleted.Name, deleted.Email, deleted.Street, deleted.City, deleted.State, deleted.Country, deleted.ZipCode
        WHERE [Id] = @Id;
      ";

    return _sqlConnector.RunInsideTransactionAsync(
      async (connection, transaction) =>
      {
        var result = await connection.QueryAsync(
          Sql,
          MapData,
          new { Id = id },
          transaction,
          splitOn: "Street"
        );

        return result.FirstOrDefault();
      }
    );
  }

  private static readonly Func<User, Address, User> MapData = (user, address) =>
  {
    user.Address = address;

    return user;
  };
}
