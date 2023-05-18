namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public static class StringEditorOptionsExtensions
{
    public static void UseSqlServer(this StringEditorOptions options, string connectionString)
    {
        options.UseDataProvider<SqlServerDataProvider>(
            new SqlServerDataProviderOptions(connectionString));
    }
    
    public static void UseSqlServer(this StringEditorOptions options, SqlServerDataProviderOptions dataProviderOptions)
    {
        options.UseDataProvider<SqlServerDataProvider>(dataProviderOptions);
    }
}