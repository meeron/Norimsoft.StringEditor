using System.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.SqlServer.Repositories;

namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public class SqlServerDataContext : IDataContext
{
    private readonly SqlServerDataProviderOptions _options;
    private SqlConnection? _connection;
    private IAppsRepository? _appsRepository;

    public SqlServerDataContext(IDataContextOptions options)
    {
        _options = (SqlServerDataProviderOptions)options;
    }

    public IAppsRepository Apps => _appsRepository ??= new AppsRepository(Connection(), _options);
    
    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
    
    private SqlConnection Connection()
    {
        if (_connection != null) return _connection;
        
        _connection = new SqlConnection(_options.ConnectionString);
        _connection.Open();

        return _connection;
    }
}
