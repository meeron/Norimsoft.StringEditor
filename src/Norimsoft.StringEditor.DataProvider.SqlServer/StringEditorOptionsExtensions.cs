using RepoDb;
using RepoDb.DbHelpers;
using RepoDb.DbSettings;
using RepoDb.StatementBuilders;

namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public static class StringEditorOptionsExtensions
{
    public static void UseSqlServer(this StringEditorOptions options, string connectionString) =>
        UseSqlServer(options, new SqlServerDataProviderOptions(connectionString));

    public static void UseSqlServer(this StringEditorOptions options, SqlServerDataProviderOptions dataProviderOptions)
    {
        options.UseDataProvider<SqlServerDataContext>(dataProviderOptions);
        options.UseMigrationProvider<SqlServerMigrationProvider>();
        
        RepoDbBootstrap();
        Mappers.Mappers.Init(dataProviderOptions);
    }

    private static void RepoDbBootstrap()
    {
        var dbSetting = new SqlServerDbSetting();
        
        GlobalConfiguration
            .Setup()
            .UseSqlServer();
        
        DbSettingMapper
            .Add<Microsoft.Data.SqlClient.SqlConnection>(dbSetting, true);
        DbHelperMapper
            .Add<Microsoft.Data.SqlClient.SqlConnection>(new SqlServerDbHelper(), true);
        StatementBuilderMapper
            .Add<Microsoft.Data.SqlClient.SqlConnection>(new SqlServerStatementBuilder(dbSetting), true);
    }
}
