using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistance.DataAccess.SqlServer;

internal sealed class SqlConnector : ISqlConnector
{
  const int DefaultCommandTimeout = 60; // Seconds

  private readonly ISqlConnectionFactory _sqlConnectionFactory;

  public SqlConnector(ISqlConnectionFactory sqlConnectionFactory)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
  }

  private async Task<TResult> UsingTransaction<TResult>(
    Func<SqlConnection, SqlTransaction, Task<TResult>> action
  )
  {
    using SqlConnection connection = await _sqlConnectionFactory.GetOpenConnectionAsync();
    using SqlTransaction transaction = connection.BeginTransaction();

    try
    {
      return await action(connection, transaction);
    }
    catch (Exception)
    {
      try
      {
        transaction.Rollback();
        throw;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }

  public Task<IEnumerable<T>> QueryAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  )
  {
    return UsingTransaction(
      async (connection, transaction) =>
      {
        var result = await connection.QueryAsync<T>(
          sql,
          param,
          transaction,
          DefaultCommandTimeout,
          commandType
        );

        transaction.Commit();

        return result;
      }
    );
  }

  public Task<T> QuerySingleAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  )
  {
    return UsingTransaction(
      async (connection, transaction) =>
      {
        var result = await connection.QuerySingleAsync<T>(
          sql,
          param,
          transaction,
          DefaultCommandTimeout,
          commandType
        );

        transaction.Commit();

        return result;
      }
    );
  }

  public Task<T?> QuerySingleOrDefaultAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  )
  {
    return UsingTransaction(
      async (connection, transaction) =>
      {
        var result = await connection.QuerySingleOrDefaultAsync<T>(
          sql,
          param,
          transaction,
          DefaultCommandTimeout,
          commandType
        );

        transaction.Commit();

        return result;
      }
    );
  }
}
