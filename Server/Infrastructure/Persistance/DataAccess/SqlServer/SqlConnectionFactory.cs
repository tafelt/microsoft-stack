using Infrastructure.Services.Settings;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistance.DataAccess.SqlServer;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  private readonly ISettingsService _settingsService;

  public SqlConnectionFactory(ISettingsService settingsService)
  {
    _settingsService = settingsService;
  }

  public SqlConnection GetOpenConnection()
  {
    var connectionString = _settingsService.GetSqlConnectionString();
    var connection = new SqlConnection(connectionString);

    connection.Open();

    return connection;
  }
}
