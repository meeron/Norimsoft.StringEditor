using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Norimsoft.StringEditor.Helpers;

namespace Norimsoft.StringEditor;

public static class ApplicationBuilderExtensions
{
    public static void UseStringEditor(this IEndpointRouteBuilder app, Action<StringEditorConfiguration> configure)
    {
        var config = new StringEditorConfiguration();
        configure(config);
        
        Configure(app, config);
    }
    
    public static void UseStringEditor(this IEndpointRouteBuilder app) =>
        Configure(app, new StringEditorConfiguration());

    private static void Configure(IEndpointRouteBuilder app, StringEditorConfiguration config)
    {
        app.MapGet(config.Path, () =>
        {
            var dataStream = EmbeddedHelpers.GetResource("index.html");

            return dataStream != null
                ? Results.File(dataStream, "text/html")
                : Results.NotFound();
        });

        // Map public endpoint used by third party to fetch translations
        app.MapGet($"{config.Path}/{{env}}/{{app}}", (string env, string app) =>
        {
            return new { env, app };
        });
    }
}