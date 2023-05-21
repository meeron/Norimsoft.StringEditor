using Microsoft.Extensions.Logging;

namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public class SqlServerMigrationProvider : IMigrationProvider
{
    private readonly ILogger<SqlServerMigrationProvider> _logger;

    public SqlServerMigrationProvider(ILogger<SqlServerMigrationProvider> logger)
    {
        _logger = logger;
    }

    public async Task Migrate()
    {
        await Task.CompletedTask;
        _logger.LogInformation("Migrating...");
        
        //TODO: Migrate logic
        
        _logger.LogInformation("Migration done.");
    }
}