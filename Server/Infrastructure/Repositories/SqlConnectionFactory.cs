using Application.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  public SqlConnection GetOpenConnection()
  {
    var connectionString = Environment.GetEnvironmentVariable("DOTNET_DEFAULT_CONNECTION_STRING");
    var connection = new SqlConnection(connectionString);

    connection.Open();

    return connection;
  }
}
