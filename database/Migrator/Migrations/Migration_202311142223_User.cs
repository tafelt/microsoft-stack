using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Database;

[Migration(202311142223)]
public class Migration_202311142223_User : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        Create
            .Table("User")
            .WithColumn("Id")
            .AsInt32()
            .NotNullable()
            .PrimaryKey()
            .Identity(1, 1)
            .WithColumn("Name")
            .AsString()
            .NotNullable()
            .WithColumn("Email")
            .AsString()
            .NotNullable();
    }
}
