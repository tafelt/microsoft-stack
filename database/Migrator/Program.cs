﻿using System.Data.SqlClient;
using System.Reflection;
using CommandLine;
using Dapper;
using database;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser
                .Default
                .ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    EnsureDatabaseExists(
                        options.SystemConnectionString,
                        options.ApplicationDatabase
                    );

                    using var serviceProvider = CreateServices(options.ApplicationConnectionString);
                    using var scope = serviceProvider.CreateScope();

                    UpdateDatabase(scope.ServiceProvider);

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
            var parameters = new DynamicParameters();
            parameters.Add("name", database);

            using var connection = new SqlConnection(connectionString);

            var records = connection.Query(
                "SELECT * FROM sys.databases WHERE name = @name",
                parameters
            );

            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {database}");
            }

            connection.Close();
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if (runner.HasMigrationsToApplyUp())
            {
                runner.ListMigrations();
                runner.MigrateUp();
            }
        }
    }
}
