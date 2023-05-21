using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor;

public class StringEditorOptions
{
    internal StringEditorOptions()
    {
    }

    public void UseDataProvider<TDataProvider>(IDataProviderOptions? options = null)
        where TDataProvider : IStringEditorDataProvider
    {
        DataProviderImplType = typeof(TDataProvider);
        DataProviderOptions = options;
    }

    public void UseMigrationProvider<TMigrationProvider>()
        where TMigrationProvider : IMigrationProvider
    {
        MigrationProviderImplType = typeof(TMigrationProvider);
    }

    public ServiceLifetime DataProviderLifetime { get; set; } = ServiceLifetime.Scoped;
    
    internal Type? DataProviderImplType { get; private set; }
    
    internal Type? MigrationProviderImplType { get; private set; }
    
    internal IDataProviderOptions? DataProviderOptions { get; private set; }
}