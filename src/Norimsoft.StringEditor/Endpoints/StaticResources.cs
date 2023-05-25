using System.Text.RegularExpressions;
using Norimsoft.StringEditor.Helpers;

namespace Norimsoft.StringEditor.Endpoints;

internal static class StaticResources
{
    private static readonly Regex ExtensionRegex = new Regex(@"\..*$");
    
    internal static MiddlewareHandler CreateMiddleware(StringEditorConfiguration config) => async (ctx, next) =>
    {
        var path = ctx.Request.Path;
        if (!path.StartsWithSegments($"{config.Path}/assets"))
        {
            await next(ctx);
            return;
        }

        var pathString = path.Value!;
        
        var dataStream = EmbeddedHelpers.GetResource(pathString.Replace("/strings/", string.Empty));
        if (dataStream == null)
        {
            await Results.NotFound().ExecuteAsync(ctx);
            return;
        }

        await Results.File(dataStream, GetContentTypeFromPath(pathString)).ExecuteAsync(ctx);
    };

    private static string GetContentTypeFromPath(string path)
    {
        var match = ExtensionRegex.Match(path.ToLower());

        return match.Value switch
        {
            ".js" => "text/javascript",
            ".css" => "text/css",
            ".ico" => "image/x-icon",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream",
        };
    }
}
