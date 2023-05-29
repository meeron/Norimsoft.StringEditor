namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public class SqlServerDataProviderOptions : IDataContextOptions
{
    public SqlServerDataProviderOptions(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { get; }

    public string Schema { get; set; } = "strings";

    public int CommandTimeout { get; set; } = 30; //seconds
}
