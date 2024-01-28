using System.Data;

namespace Infrastructure.Persistance.DataAccess.SqlServer;

public interface ISqlConnector
{
  Task<IEnumerable<T>> QueryAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  );

  Task<T?> QuerySingleOrDefaultAsync<T>(
    string sql,
    object? param = null,
    CommandType? commandType = null
  );

  Task<T> QuerySingleAsync<T>(string sql, object? param = null, CommandType? commandType = null);
}
