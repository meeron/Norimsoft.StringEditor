namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public static class StringEditorOptionsExtensions
{
    public static void UseSqlServer(this StringEditorOptions options, string connectionString) =>
        UseSqlServer(options, new SqlServerDataProviderOptions(connectionString));

    public static void UseSqlServer(this StringEditorOptions options, SqlServerDataProviderOptions dataProviderOptions)
    {
        options.UseDataProvider<SqlServerDataContext>(dataProviderOptions);
        options.UseMigrationProvider<SqlServerMigrationProvider>();
    }
}
