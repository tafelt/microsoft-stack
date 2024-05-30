using Infrastructure.Services.Settings;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess.SqlServer;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  private readonly ISettingsService _settingsService;

  public SqlConnectionFactory(ISettingsService settingsService)
  {
    _settingsService = settingsService;
  }

  public async Task<SqlConnection> GetOpenConnectionAsync()
  {
    var connectionString = _settingsService.GetSqlConnectionString();
    var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    return connection;
  }
}
