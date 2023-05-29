using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor;

public class StringEditorOptions
{
    internal StringEditorOptions()
    {
    }

    public void UseDataProvider<TDataContext>(IDataContextOptions? options = null)
        where TDataContext : IDataContext
    {
        DataContextImplType = typeof(TDataContext);
        DataContextOptions = options;
    }

    public void UseMigrationProvider<TMigrationProvider>()
        where TMigrationProvider : IMigrationProvider
    {
        MigrationProviderImplType = typeof(TMigrationProvider);
    }

    public ServiceLifetime DataProviderLifetime { get; set; } = ServiceLifetime.Scoped;
    
    internal Type? DataContextImplType { get; private set; }
    
    internal Type? MigrationProviderImplType { get; private set; }
    
    internal IDataContextOptions? DataContextOptions { get; private set; }
}
