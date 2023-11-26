using Application.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  private readonly IConfiguration _configuration;

  public SqlConnectionFactory(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public SqlConnection CreateConnection()
  {
    // TODO: Get connection string from environment variables
    return new SqlConnection(
      "Data Source=mssql;Database=appdb;User ID=sa;Password=<YourStrong!Passw0rd>;Trusted_Connection=false;TrustServerCertificate=true;"
    // _configuration.GetConnectionString(
    //   "DOTNET_APP_DATABASE_CONNECTION_STRING"
    // )
    );
  }
}
