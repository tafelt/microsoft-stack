using Infrastructure.Persistance.DataAccess.Common;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistance.DataAccess.SqlServer;

public interface ISqlConnectionFactory : IDbConnectionFactory<SqlConnection> { }
