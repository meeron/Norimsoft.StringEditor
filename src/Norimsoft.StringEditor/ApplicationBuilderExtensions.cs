using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.Helpers;

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
        app.MapGet(config.Path, () =>
        {
            var dataStream = EmbeddedHelpers.GetResource("index.html");

            return dataStream != null
                ? Results.File(dataStream, "text/html")
                : Results.NotFound();
        });

        // Map public endpoint used by third party to fetch translations
        app.MapGet($"{config.Path}/{{env}}/{{appName}}", (string env, string appName) =>
        {
            return new { env, app };
        });
        
        using var scope = app.Services.CreateScope();
        var migrationProvider = scope.ServiceProvider.GetService<IMigrationProvider>();

        migrationProvider?.Migrate().Wait();
    }
}