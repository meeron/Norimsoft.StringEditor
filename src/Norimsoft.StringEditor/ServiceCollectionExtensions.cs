using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;
using Slugify;

namespace Norimsoft.StringEditor;

public static class ServiceCollectionExtensions
{
    public static void AddStringEditor(this IServiceCollection services, Action<StringEditorOptions> configure)
    {
        var stringEditorOptions = new StringEditorOptions();
        configure(stringEditorOptions);
        
        ArgumentNullException.ThrowIfNull(stringEditorOptions.DataContextImplType);

        if (stringEditorOptions.DataContextOptions != null)
        {
            services.AddSingleton(stringEditorOptions.DataContextOptions);
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
                    typeof(IDataContext),
                    stringEditorOptions.DataContextImplType);
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped(
                    typeof(IDataContext),
                    stringEditorOptions.DataContextImplType);
                break;
            case ServiceLifetime.Transient:
                services.AddTransient(
                    typeof(IDataContext),
                    stringEditorOptions.DataContextImplType);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        services.AddSingleton<ISlugHelper, SlugHelper>();
        services.AddValidatorsFromAssemblyContaining<ErrorCodeResult>();
    }
}
