using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.Endpoints;
using Norimsoft.StringEditor.Endpoints.Api;

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
        var mainGroup = app.MapGroup(config.Path);
        mainGroup.MapGet("", HomeEndpoint.Handler);
        mainGroup.MapGet("{env}/{appName}", GetStringsEndpoint.Handler);

        var apiV1Group = mainGroup.MapGroup("api/v1");
        apiV1Group.MapGet("apps", GetAppsEndpoint.Handler);
        
        if (!config.RunMigration) return;
        
        using var scope = app.Services.CreateScope();
        var migrationProvider = scope.ServiceProvider.GetService<IMigrationProvider>();

        migrationProvider?.Migrate().Wait();
    }
}
