using CommandLine;

namespace database;

public class Options
{
    [Option("masterConnectionString", Required = true)]
    public string SystemConnectionString { get; set; }

    [Option("applicationConnectionString", Required = true)]
    public string ApplicationConnectionString { get; set; }

    [Option("applicationDatabase", Required = true)]
    public string ApplicationDatabase { get; set; }
}
