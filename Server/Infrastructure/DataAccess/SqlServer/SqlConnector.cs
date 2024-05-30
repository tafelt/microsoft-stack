using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess.SqlServer;

internal sealed class SqlConnector : ISqlConnector
{
  private readonly ISqlConnectionFactory _sqlConnectionFactory;

  public SqlConnector(ISqlConnectionFactory sqlConnectionFactory)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
  }

  public async Task<TResult> RunInsideTransactionAsync<TResult>(
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
      }
      catch (Exception)
      {
        throw;
      }

      throw;
    }
  }

  public async Task<TResult> RunAsync<TResult>(Func<SqlConnection, Task<TResult>> action)
  {
    using SqlConnection connection = await _sqlConnectionFactory.GetOpenConnectionAsync();

    try
    {
      return await action(connection);
    }
    catch (Exception)
    {
      throw;
    }
  }
}
