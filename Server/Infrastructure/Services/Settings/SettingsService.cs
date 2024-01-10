namespace Infrastructure.Services.Settings;

internal sealed class SettingsService : ISettingsService
{
  public string GetSqlConnectionString()
  {
    return Environment.GetEnvironmentVariable(EnvironmentVariableKeys.SqlConnectionString)
      ?? throw new ArgumentNullException(
        $"Could not get value for '{EnvironmentVariableKeys.SqlConnectionString}' environment variable."
      );
  }
}
