using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistance.DataAccess.SqlServer;

public interface ISqlConnectionFactory
{
  Task<SqlConnection> GetOpenConnectionAsync();
}
