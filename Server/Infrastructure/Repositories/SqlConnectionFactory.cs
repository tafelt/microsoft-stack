using Application.Abstractions;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  public SqlConnection CreateConnection()
  {
    return new SqlConnection(
      Environment.GetEnvironmentVariable("DOTNET_DEFAULT_CONNECTION_STRING")
    );
  }
}
