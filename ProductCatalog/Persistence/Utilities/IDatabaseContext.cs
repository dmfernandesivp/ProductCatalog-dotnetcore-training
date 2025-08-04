using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Persistence.Utilities;

public interface IDatabaseContext
{
    IDbConnection CreateConnection();
}

public class DatabaseContext : IDatabaseContext
{
    private readonly string _connectionString;

    public DatabaseContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string not found");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
