using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistance.DataAccess.SqlServer;

public interface ISqlConnectionFactory
{
  SqlConnection GetOpenConnection();
}
