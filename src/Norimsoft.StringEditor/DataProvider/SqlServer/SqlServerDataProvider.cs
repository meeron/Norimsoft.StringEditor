namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public class SqlServerDataProvider : IStringEditorDataProvider
{
    private readonly SqlServerDataProviderOptions _options;

    public SqlServerDataProvider(IDataProviderOptions options)
    {
        _options = (SqlServerDataProviderOptions)options;
    }
}