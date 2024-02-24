namespace Infrastructure.Persistance.DataAccess.Common;

public interface IDbConnectionFactory<TDbConnection>
{
  Task<TDbConnection> GetOpenConnectionAsync();
}
