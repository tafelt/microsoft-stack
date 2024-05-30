using FluentMigrator;

namespace Database;

[Migration(202405091532)]
public class Migration_202405091532_User_Address : Migration
{
  public override void Down()
  {
    throw new NotImplementedException();
  }

  public override void Up()
  {
    Alter
      .Table("User")
      .AddColumn("Street")
      .AsString()
      .NotNullable()
      .WithDefaultValue(string.Empty)
      .AddColumn("City")
      .AsString()
      .NotNullable()
      .WithDefaultValue(string.Empty)
      .AddColumn("State")
      .AsString()
      .NotNullable()
      .WithDefaultValue(string.Empty)
      .AddColumn("Country")
      .AsString()
      .NotNullable()
      .WithDefaultValue(string.Empty)
      .AddColumn("ZipCode")
      .AsString()
      .NotNullable()
      .WithDefaultValue(string.Empty);
  }
}
