using System.Text.RegularExpressions;
using Norimsoft.StringEditor.Extensions;
using Norimsoft.StringEditor.Helpers;

namespace Norimsoft.StringEditor.Middlewares;

public class StaticResourcesMiddleware
{
    private static readonly Regex ExtensionRegex = new Regex(@"\..*$");
    
    private readonly RequestDelegate _next;

    public StaticResourcesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        var config = ctx.GetConfig();
        
        var path = ctx.Request.Path;
        if (!path.StartsWithSegments($"{config.Path}/assets"))
        {
            await _next(ctx);
            return;
        }

        var pathString = path.Value!;
        
        var dataStream = EmbeddedHelpers.GetResource(pathString.Replace($"{config.Path}/", string.Empty));
        if (dataStream == null)
        {
            await Results.NotFound().ExecuteAsync(ctx);
            return;
        }

        await Results.File(dataStream, GetContentTypeFromPath(pathString)).ExecuteAsync(ctx);
    }
    
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
