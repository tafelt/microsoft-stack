using System.Data;

namespace Infrastructure.DataAccess.Common;

public interface IDbConnector<TDbConnection, TDbTransaction>
  where TDbConnection : IDbConnection
  where TDbTransaction : IDbTransaction
{
  Task<TResult> RunInsideTransactionAsync<TResult>(
    Func<TDbConnection, TDbTransaction, Task<TResult>> action
  );

  Task<TResult> RunAsync<TResult>(Func<TDbConnection, Task<TResult>> action);
}
