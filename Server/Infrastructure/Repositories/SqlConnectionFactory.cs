using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  public SqlConnection GetOpenConnection()
  {
    var connectionString = Environment.GetEnvironmentVariable("MSSQL_APP_CONNECTION_STRING");
    var connection = new SqlConnection(connectionString);

    connection.Open();

    return connection;
  }
}
