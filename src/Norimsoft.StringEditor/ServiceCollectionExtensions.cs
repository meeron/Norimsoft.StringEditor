using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor;

public static class ServiceCollectionExtensions
{
    public static void AddStringEditor(this IServiceCollection services, Action<StringEditorOptions> configure)
    {
        var stringEditorOptions = new StringEditorOptions();
        configure(stringEditorOptions);
        
        ArgumentNullException.ThrowIfNull(stringEditorOptions.DataProviderImplType);

        if (stringEditorOptions.DataProviderOptions != null)
        {
            services.AddSingleton(stringEditorOptions.DataProviderOptions);
        }

        if (stringEditorOptions.MigrationProviderImplType != null)
        {
            services.AddTransient(
                typeof(IMigrationProvider),
                stringEditorOptions.MigrationProviderImplType);
        }

        switch (stringEditorOptions.DataProviderLifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton(
                    typeof(IStringEditorDataProvider),
                    stringEditorOptions.DataProviderImplType);
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped(
                    typeof(IStringEditorDataProvider),
                    stringEditorOptions.DataProviderImplType);
                break;
            case ServiceLifetime.Transient:
                services.AddTransient(
                    typeof(IStringEditorDataProvider),
                    stringEditorOptions.DataProviderImplType);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}