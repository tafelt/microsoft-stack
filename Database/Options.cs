using CommandLine;

namespace Database;

public class Options
{
  [Option("systemConnectionString", Required = true)]
  public string SystemConnectionString { get; set; }

  [Option("baseConnectionString", Required = true)]
  public string BaseConnectionString { get; set; }

  [Option("database", Required = true)]
  public string Database { get; set; }
}
