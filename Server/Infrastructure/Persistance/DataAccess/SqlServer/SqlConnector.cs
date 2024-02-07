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

  private async Task<TResult> QueryUsingTransaction<TResult>(
    Func<SqlConnection, SqlTransaction, Task<TResult>> action
  )
  {
    using SqlConnection connection = await _sqlConnectionFactory.GetOpenConnectionAsync();
    using SqlTransaction transaction = connection.BeginTransaction();

    try
    {
      var result = await action(connection, transaction);

      transaction.Commit();

      return result;
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
    return QueryUsingTransaction(
      async (connection, transaction) =>
        await connection.QueryAsync<T>(sql, param, transaction, DefaultCommandTimeout, commandType)
    );
  }

  public Task<T> QuerySingleAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  )
  {
    return QueryUsingTransaction(
      async (connection, transaction) =>
        await connection.QuerySingleAsync<T>(
          sql,
          param,
          transaction,
          DefaultCommandTimeout,
          commandType
        )
    );
  }

  public Task<T?> QuerySingleOrDefaultAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  )
  {
    return QueryUsingTransaction(
      async (connection, transaction) =>
        await connection.QuerySingleOrDefaultAsync<T>(
          sql,
          param,
          transaction,
          DefaultCommandTimeout,
          commandType
        )
    );
  }
}
