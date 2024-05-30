using CommandLine;

namespace Database;

public class Options
{
  [Option("systemConnectionString", Required = true, HelpText = "Connection string used to access system databases.")]
  public string SystemConnectionString { get; set; }

  [Option("appConnectionString", Required = true, HelpText = "Connection string used to access application database.")]
  public string AppConnectionString { get; set; }

  [Option("database", Required = true, HelpText = "Database name.")]
  public string Database { get; set; }
}