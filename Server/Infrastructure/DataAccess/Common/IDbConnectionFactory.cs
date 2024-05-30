using System.Data;

namespace Infrastructure.DataAccess.Common;

public interface IDbConnectionFactory<TDbConnection>
  where TDbConnection : IDbConnection
{
  Task<TDbConnection> GetOpenConnectionAsync();
}
