using Infrastructure.DataAccess.Common;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess.SqlServer;

public interface ISqlConnector : IDbConnector<SqlConnection, SqlTransaction> { }
