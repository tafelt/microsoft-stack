using System.Data;

namespace Infrastructure.Persistance.DataAccess.Common;

public interface IDbConnector
{
  Task<IEnumerable<TResult>> QueryAsync<TResult>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  );

  Task<TResult?> QuerySingleOrDefaultAsync<TResult>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  );

  Task<TResult> QuerySingleAsync<TResult>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  );
}
