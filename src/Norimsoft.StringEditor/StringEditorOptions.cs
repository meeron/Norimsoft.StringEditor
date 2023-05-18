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

    public ServiceLifetime DataProviderLifetime { get; set; } = ServiceLifetime.Scoped;
    
    internal Type? DataProviderImplType { get; private set; }
    
    internal IDataProviderOptions? DataProviderOptions { get; private set; }
}