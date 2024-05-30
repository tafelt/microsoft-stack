using System.Data.SqlClient;
using System.Reflection;
using CommandLine;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

class Program
{
  static void Main(string[] args)
  {
    Parser
      .Default
      .ParseArguments<Options>(args)
      .WithParsed(options =>
      {
        EnsureDatabaseExists(options.SystemConnectionString, options.Database);

        using var serviceProvider = CreateServices(options.AppConnectionString);
        using var scope = serviceProvider.CreateScope();

        RunMigrations(scope.ServiceProvider);

        Environment.Exit(0);
      });
  }

  private static ServiceProvider CreateServices(string connectionString)
  {
    return new ServiceCollection()
      .AddFluentMigratorCore()
      .ConfigureRunner(
        configure =>
          configure
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.GetExecutingAssembly())
            .For
            .All()
      )
      .AddLogging(configure => configure.AddFluentMigratorConsole())
      .BuildServiceProvider(false);
  }

  private static void EnsureDatabaseExists(string connectionString, string database)
  {
    using var connection = new SqlConnection(connectionString);
    
    var records = connection.Query("SELECT * FROM sys.databases WHERE name = @Database;", new { Database = database });

    if (!records.Any())
    {
      connection.Execute($"CREATE DATABASE {database};");
    }

    connection.Close();
  }

  private static void RunMigrations(IServiceProvider serviceProvider)
  {
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    if (runner.HasMigrationsToApplyUp())
    {
      runner.ListMigrations();
      runner.MigrateUp();
    }
  }
}
