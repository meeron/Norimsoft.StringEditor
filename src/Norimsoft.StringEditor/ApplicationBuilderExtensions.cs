using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.Endpoints;
using Norimsoft.StringEditor.Endpoints.Apps;
using Norimsoft.StringEditor.Endpoints.Languages;
using Norimsoft.StringEditor.Middlewares;

namespace Norimsoft.StringEditor;

public static class ApplicationBuilderExtensions
{
    public static void UseStringEditor(this WebApplication app, Action<StringEditorConfiguration> configure)
    {
        var config = new StringEditorConfiguration();
        configure(config);
        
        UseStringEditorRoot(app, config);
    }
    
    public static void UseStringEditor(this WebApplication app) =>
        UseStringEditorRoot(app, new StringEditorConfiguration());

    private static void UseStringEditorRoot(WebApplication app, StringEditorConfiguration config)
    {
        app.UseMiddleware<StringEditorConfigurationMiddleware>(config);
        app.UseMiddleware<StaticResourcesMiddleware>();

        var mainGroup = app.MapGroup(config.Path);
        mainGroup.MapGet("", HomeEndpoint.Handler);
        mainGroup.MapGet("{env}/{appName}", GetStringsEndpoint.Handler);

        mainGroup.MapAppsEndpoints();
        mainGroup.MapLanguagesEndpoints();
        
        RunMigrationIfRequired(app, config);
    }

    private static void RunMigrationIfRequired(WebApplication app, StringEditorConfiguration config)
    {
        if (!config.RunMigration) return;
        
        using var scope = app.Services.CreateScope();
        var migrationProvider = scope.ServiceProvider.GetService<IMigrationProvider>();

        migrationProvider?.Migrate().Wait();
    }
    
}
