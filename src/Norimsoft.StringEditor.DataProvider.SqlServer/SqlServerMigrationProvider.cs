using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public class SqlServerMigrationProvider : IMigrationProvider
{
    private const string BaseResourceName = "Norimsoft.StringEditor.DataProvider.SqlServer.Scripts";

    private readonly SqlServerDataProviderOptions _options;
    private readonly SqlConnection _connection;
    private readonly ILogger<SqlServerMigrationProvider> _logger;

    public SqlServerMigrationProvider(
        IDataContextOptions options,
        ILogger<SqlServerMigrationProvider> logger)
    {
        _logger = logger;
        _options = (SqlServerDataProviderOptions)options;
        _connection = new SqlConnection(_options.ConnectionString);
    }

    public async Task Migrate()
    {
        _logger.LogInformation("Migrating...");
        await _connection.OpenAsync();
        await using var transaction = _connection.BeginTransaction();

        var initScript = await GetScript("01-Init");
        initScript = initScript.Replace("#schema#", _options.Schema);

        await RunScript(initScript, transaction);

        await transaction.CommitAsync();
        _logger.LogInformation("Migration done.");
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }

    private static async Task<string> GetScript(string name)
    {
        var asm = Assembly.GetExecutingAssembly();
        var resourceName = $"{BaseResourceName}.{name}.sql";
        using var reader = new StreamReader(asm.GetManifestResourceStream(resourceName)!);

        return await reader.ReadToEndAsync();
    }

    private async Task RunScript(string script, SqlTransaction transaction)
    {
        await using var cmd = new SqlCommand(script)
        {
            Connection = _connection,
            Transaction = transaction,
            CommandTimeout = _options.CommandTimeout,
        };
        
        await cmd.ExecuteNonQueryAsync();
    }

}
